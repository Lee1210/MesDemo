using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Newtonsoft.Json.Linq;

using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc.Binders;
using OSharp.Web.Mvc.Security;
using OSharp.Web.UI;


namespace OSharp.Demo.Web.Areas.Admin.Controllers
{
    public class ResController : AdminBaseController
    {
        public IResContract _ResContract { get; set; }

        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            if (id.HasValue && id.Value > 0)
            {
                request.FilterGroup.Rules.Add(new FilterRule("Organization.Id", id.Value));
            }
            var datas = GetQueryData<Res, int>(_ResContract.Res, out total, request).Select(m => new
            {
                m.Id,
                m.Name,
                m.Remark,
                m.CreatedTime,
                m.ResCode,
                m.IsDeleted
            });
            
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<ResDto>))] ICollection<ResDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = _ResContract.AddRes(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<ResDto>))] ICollection<ResDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = _ResContract.EditRes(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = _ResContract.DeleteRes(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }
	}
}