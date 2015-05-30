using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Newtonsoft.Json.Linq;

using OSharp.Core.Data.Entity;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Binders;
using OSharp.Web.Mvc.Security;
using OSharp.Web.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    public class DelayTimeController : AdminBaseController
    {
        public IDelayTimeContract DelayTimeContract { get; set; }

        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            if (id.HasValue && id.Value > 0)
            {
                request.FilterGroup.Rules.Add(new FilterRule("Organization.Id", id.Value));
            }
            var datas = GetQueryData<DelayTime, int>(DelayTimeContract.DelayTimes, out total, request).Select(m => new
            {
                m.Id,
                m.Line,
                m.Duty,
                m.CreatedTime,
                m.Hour,
                m.WorkDate,
                m.IsDeleted
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<DelayTimeDto>))] ICollection<DelayTimeDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = DelayTimeContract.AddDelayTime(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<DelayTimeDto>))] ICollection<DelayTimeDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = DelayTimeContract.EditDelayTime(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = DelayTimeContract.DeleteDelayTime(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }



        public virtual ActionResult Report1()
        {

            //ViewBag.data1 = Json(new string[] { "衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子" }, JsonRequestBehavior.AllowGet);
            //ViewBag.data2 = Json(new int[] { 5, 20, 60, 10, 10, 20 }, JsonRequestBehavior.AllowGet);

            ViewBag.data1 = new string[] { "衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子" }.ToJsonString();
            ViewBag.data2 = new int[] { 5, 20, 60, 10, 10, 20 }.ToJsonString();
            return View();
        }

        public virtual ActionResult Report2()
        {

            //ViewBag.data1 = Json(new string[] { "衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子" }, JsonRequestBehavior.AllowGet);
            //ViewBag.data2 = Json(new int[] { 5, 20, 60, 10, 10, 20 }, JsonRequestBehavior.AllowGet);

            var data1 = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天" };
            var data2 = new int[] { 11, 11, 15, 13, 12, 13, 10 };
            var jobect = new JObject(
                new JProperty("data1", data1),
                new JProperty("data2", data2)
                );
            return View();
        }

        [AjaxOnly]
        public virtual ActionResult Report3(string fromDate, string toDate, string field)
        {
            field = "WorkDate";
            string field2 = "Line";
            toDate.CheckNotNullOrEmpty("fromDate");
            toDate.CheckNotNullOrEmpty("toDate");
            // ReSharper disable once InconsistentNaming
            int _fromDate = Convert.ToInt32(fromDate.Replace("-", ""));
            // ReSharper disable once InconsistentNaming
            int _toDate = Convert.ToInt32(toDate.Replace("-", ""));
            //x坐标节点
            var xAxisdata = new ArrayList();
            //部门辅助开关
            var legenddata = new ArrayList();
            //标题
            const string titletext = "异常工时";
            //子标题
            var titlesubtext = _fromDate + "-" + _toDate;
            FilterGroup group = new FilterGroup
            {
                Rules = new List<FilterRule> 
                { new FilterRule(field, _fromDate, FilterOperate.Greater), 
                  new FilterRule(field, _toDate, FilterOperate.LessOrEqual) },
                Operate = FilterOperate.And
            };
            Expression<Func<DelayTime, bool>> predicate = FilterHelper.GetExpression<DelayTime>(group);
            var delayTimes = DelayTimeContract.DelayTimes.Where(predicate).ToList();

            var dutyList = delayTimes.Select(a => a.Duty).Distinct();

            var dutyWorkdateHourList = new ArrayList();
            for (int i = 0; i < _toDate - _fromDate + 1; i++)
            {
                xAxisdata.Add(_fromDate + i);
            }
            foreach (var duty in dutyList)
            {
                var dutyhourlist = new ArrayList();
                for (int i = 0; i < _toDate - _fromDate + 1; i++)
                {
                    FilterGroup group2 = new FilterGroup
                    {
                        Rules = new List<FilterRule> 
                            { new FilterRule(field2, duty, FilterOperate.Equal), 
                              new FilterRule(field, _fromDate + i, FilterOperate.Equal) },
                        Operate = FilterOperate.And
                    };
                    Expression<Func<DelayTime, bool>> predicate2 = FilterHelper.GetExpression<DelayTime>(group2);
                    float b = delayTimes.AsQueryable().Where(predicate2).Sum(g => g.Hour);
                    dutyhourlist.Add(b);
                }
                var dutyWorkdateHour = new { duty = duty, dutyhourlist = dutyhourlist };
                dutyWorkdateHourList.Add(dutyWorkdateHour);
            }
            var seriesList = new ArrayList();
            foreach (dynamic dutyWorkdateHour in dutyWorkdateHourList)
            {
                var markPointData1 = new { type = "max", name = "最大值" };
                var markPointData2 = new { type = "min", name = "最小值" };
                var markPointList = new ArrayList() { markPointData1, markPointData2 };
                var markPoint = new { data = markPointList };

                var markLineData1 = new { type = "average", name = "平均值" };
                var markLineList = new ArrayList() { markLineData1 };
                var markLine = new { data = markLineList };
                var seriestest1 = new { name = dutyWorkdateHour.duty, type = "line", data = dutyWorkdateHour.dutyhourlist, markPoint = markPoint, markLine = markLine };
                legenddata.Add(dutyWorkdateHour.duty);
                seriesList.Add(seriestest1);

            }
            var data = new { seriesList = seriesList, xAxisdata = xAxisdata, legenddata = legenddata, titletext = titletext, titlesubtext = titlesubtext };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}