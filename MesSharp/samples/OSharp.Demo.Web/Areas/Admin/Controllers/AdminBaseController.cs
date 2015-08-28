

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Core.Data;
using Mes.Core.Data.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理控制器蕨类
    /// </summary>
    public abstract class AdminBaseController : BaseController
    {
        protected virtual IQueryable<TEntity> GetQueryData<TEntity, TKey>(IQueryable<TEntity> source, out int total, GridRequest request = null)
            where TEntity : EntityBase<TKey>
        {
            if (request == null)
            {
                request = new GridRequest(Request);
            }
            Expression<Func<TEntity, bool>> predicate = FilterHelper.GetExpression<TEntity>(request.FilterGroup);
            return source.Where<TEntity, TKey>(predicate, request.PageCondition, out total);
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        private bool EventTypeFilter(System.Reflection.PropertyInfo p)
        {
            var attribute = Attribute.GetCustomAttribute(p,
                typeof(AssociationAttribute)) as AssociationAttribute;

            if (attribute == null) return true;
            if (attribute.IsForeignKey == false) return true;

            return false;
        }

        private object GetPropertyValue(object o)
        {
            if (o == null)
                return DBNull.Value;
            return o;
        }
        public virtual void Excel<T>(IEnumerable<T> entities, string fileName)
        {
            var response = Response;
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            string colHeaders = "", lsItem = "";
            //定义表对象与行对象，同时用DataSet对其值进行初始化
          //  DataSet ds = new DataSet();

            DataTable table = new DataTable();
            Type t = typeof(T);
            var properties = t.GetProperties().Where(EventTypeFilter).ToArray();
            // DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的

            foreach (var property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                table.Columns.Add(new DataColumn(property.Name, propertyType));
            }

            foreach (var entity in entities)
            {
                table.Rows.Add(properties.Select(
                  property => GetPropertyValue(
                  property.GetValue(entity, null))).ToArray());
            }


            int i;
            int cl = table.Columns.Count;


            //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加\n
                {
                    colHeaders += table.Columns[i].Caption + "\n";
                }
                else
                {
                    colHeaders += table.Columns[i].Caption + "\t";
                }

            }
            response.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息

            //逐行处理数据  
            foreach (DataRow row in table.Rows)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据    
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加\n
                    {
                        lsItem += row[i] + "\n";
                    }
                    else
                    {
                        lsItem += row[i] + "\t";
                    }

                }
                response.Write(lsItem);
                lsItem = "";

            }
            response.End();
        }
    }
}