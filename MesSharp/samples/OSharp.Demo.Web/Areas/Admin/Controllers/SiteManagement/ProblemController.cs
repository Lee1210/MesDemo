using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Demo.Contracts.SiteManagement;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Models.Identity;
using Mes.Demo.Models.SiteManagement;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;

using Util;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class ProblemController : AdminBaseController
    {
        public ISiteManagementContract SiteManagementContract { get; set; }


        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<Problem, int>(SiteManagementContract.Problems, out total, request).
                Where(m=>m.IsDeleted==false).
                Select(m => new
            {
                m.ProblemTime,
                m.Department,
                m.Factory,
                m.QuestionFrom,
                m.Detail,
                m.Reason,
                m.Solution,
                m.IsComplete,
                m.IsPeople,
                m.Workers,
                m.Suggestion,
                m.Remark,
                m.Type,
                m.Id,
                m.CreatedTime
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<ProblemDto>))] ICollection<ProblemDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result;
            //登录超时，重定向首页
            User user = (User)Session["user"];
            if (user == null)
            {
                result = new OperationResult(OperationResultType.Error);
                result.Message = "超时,请重新登陆";
            }
            else
            {
                foreach (var problemDto in dtos)
                {
                    problemDto.Workers = user.NickName;
                }
                result = SiteManagementContract.AddProblems(dtos.ToArray());
            }
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<ProblemDto>))] ICollection<ProblemDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SiteManagementContract.EditProblems(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result= SiteManagementContract.DeleteProblems_false(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult DepartmentReport(string fromDate,string endDate)
        {
            var data = Data(fromDate,endDate, "department");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult DepartmentList()
        {
            var data = SiteManagementContract.Departments.ToList().Select(d => new
            {
                text=d.Text,
                value=d.Value
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult ProblemTypeList()
        {
            var data = SiteManagementContract.ProblemTypes.ToList().Select(d => new
            {
                text = d.Text,
                value = d.Value
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult FactoryList()
        {
            var data = SiteManagementContract.Factorys.ToList().Select(d => new
            {
                text = d.Text,
                value = d.Value
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult ProblemSourceList()
        {
            var data = SiteManagementContract.ProblemSources.ToList().Select(d => new
            {
                text = d.Text,
                value = d.Value
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult WorkersReport(string fromDate, string endDate)
        {
            var data = Data(fromDate,endDate, "workers");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private object Data(string fromDate,string endDate,string groupByFiled)
        {
            const string problemTime = "ProblemTime";
            const string isDeleted = "IsDeleted";
            fromDate.CheckNotNullOrEmpty("fromDate");
            endDate.CheckNotNullOrEmpty("endDate");
          //  DateTime date = Conv.ToDate(fromDate);
            DateTime beginTime=Conv.ToDate(fromDate);
            DateTime endTime = Conv.ToDate(endDate); 
            //if (date.DayOfWeek != 0)
            //{
            //    beginTime = date.AddDays((double)(1 - date.DayOfWeek));
            //    endTime = date.AddDays((double)(8 - date.DayOfWeek));
            //}
            //else
            //{
            //    endTime = date;
            //    beginTime = date.AddDays(-6);
            //}

            var xAxisdata = new ArrayList();
            //部门辅助开关
            var legenddata = new ArrayList();
            //标题
            const string titletext = "异常统计";
            //子标题
            var titlesubtext = beginTime.ToString("yyyyMMdd") + "-" + endTime.ToString("yyyyMMdd");
            for (int i = 0; i < endTime.DayOfYear - beginTime.DayOfYear+1; i++)
            {
                xAxisdata.Add(beginTime.AddDays(i).ToDateString());
            }

            FilterGroup group = new FilterGroup
            {
                Rules = new List<FilterRule>
                {
                    new FilterRule(problemTime, beginTime, FilterOperate.GreaterOrEqual),
                    new FilterRule(problemTime, endTime.AddDays(1), FilterOperate.LessOrEqual),
                    new FilterRule(isDeleted, false, FilterOperate.Equal)
                },
                Operate = FilterOperate.And
            };
            Expression<Func<Problem, bool>> predicate = FilterHelper.GetExpression<Problem>(@group);
            var delayTimes = SiteManagementContract.Problems.Where(predicate).ToList();
            IEnumerable<IGrouping<string, Problem>> dutyGroupList;
            //分组
            if (groupByFiled=="workers")
                dutyGroupList = delayTimes.GroupBy(g => g.Workers);
            else
                dutyGroupList = delayTimes.GroupBy(g => g.Department);

            var seriesList = new ArrayList();
            foreach (var dutyGroup in dutyGroupList)
            {
                legenddata.Add(dutyGroup.Key);
                var dutyhourlist = new ArrayList();

                for (int i = 0; i < endTime.DayOfYear - beginTime.DayOfYear + 1; i++)
                {
                    int b = dutyGroup.AsQueryable().Count(g => g.ProblemTime.ToDateString() == beginTime.AddDays(i).ToDateString());
                    dutyhourlist.Add(b);
                }
                var markPointData1 = new { type = "max", name = "最大值" };
                var markPointData2 = new { type = "min", name = "最小值" };
                var markPointList = new ArrayList() { markPointData1, markPointData2 };
                var markPoint = new { data = markPointList };

                var markLineData1 = new { type = "average", name = "平均值" };
                var markLineList = new ArrayList() { markLineData1 };
                var markLine = new { data = markLineList };
                var seriestest1 = new { name = dutyGroup.Key, type = "line", data = dutyhourlist, markPoint, markLine };

                seriesList.Add(seriestest1);
            }

            var data = new { seriesList, xAxisdata, legenddata, titletext, titlesubtext };
            return data;
        }

        public ActionResult Report()
        {
            return View();
        }
	}
}