

using System;
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
    }
}