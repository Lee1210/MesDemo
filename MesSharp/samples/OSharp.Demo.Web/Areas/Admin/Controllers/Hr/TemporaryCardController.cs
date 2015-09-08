﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Demo.Contracts.TestLog;
using Mes.Demo.Models.Hr;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class TemporaryCardController : AdminBaseController
    {
        public IHrContract HrContract { get; set; }



        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<TemporaryCard, int>(HrContract.TemporaryCards, out total, request).Select(m => new
            {
                m.Id,
                m.Card,
                m.EmpNo,
                m.EmpName
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<TemporaryCardDto>))] ICollection<TemporaryCardDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = HrContract.AddTemporaryCards(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<TemporaryCardDto>))] ICollection<TemporaryCardDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = HrContract.EditTemporaryCards(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = HrContract.DeleteTemporaryCards(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public override void CreateExcel()
        {
            GridRequest request = new GridRequest(Request);
            var filterGroup = request.FilterGroup;
            Expression<Func<TemporaryCard, bool>> predicate = FilterHelper.GetExpression<TemporaryCard>(filterGroup);
            var temporaryCards = HrContract.TemporaryCards.Where(predicate).Select(m => new
            {
                m.Card,
                m.EmpNo,
                m.EmpName,
              
            });

            Excel(temporaryCards, "temporaryCards" + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }
    }
}
