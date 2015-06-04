

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mes.Demo.Contracts;
using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Utility;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class RolesController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 身份认证业务对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        #region Ajax功能

        #region 获取数据

        //id: 组织机构编号
        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
         
            var datas = GetQueryData<Role, int>(IdentityContract.Roles, out total, request).Select(m => new
            {
                m.Id,
                m.Name,
                m.Remark,
                m.IsAdmin,
                m.IsLocked,
                m.CreatedTime,
              
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 功能方法

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<RoleDto>))] ICollection<RoleDto> dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = IdentityContract.AddRoles(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<RoleDto>))] ICollection<RoleDto> dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = IdentityContract.EditRoles(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids" );
            OperationResult result = IdentityContract.DeleteRoles(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers()
        {
            var users = IdentityContract.Users.ToList();
            List<object> data = new List<object>();
            foreach (var user in users)
            {
                var item = new { Id = user.Id, Name = user.Name };
                data.Add(item);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Role2User(int? id)
        {
            var firstOrDefault = IdentityContract.Roles.FirstOrDefault(u => u.Id == id);
            if (firstOrDefault != null)
            {
                var data = firstOrDefault.Users.Select(r => r.Id);
                ViewBag.SelectedId = string.Join(",", data);
            }
            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult EditRole2User(int userId, int[] selectId)
        {
            OperationResult result = IdentityContract.SetRoleUsers(userId, selectId);
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}