using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Mes.Demo.Contracts;
using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Utility;
using Mes.Utility.Data;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        public IIdentityContract IdentityContract { get; set; }
  

        [AjaxOnly]
        public ActionResult GridData()
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<User, int>(IdentityContract.Users, out total, request).Select(m => new
            {
                m.Name,
                m.NickName,
                m.Password,
                m.IsLocked,
                m.Id,
                m.IsDeleted,
                m.CreatedTime, 
                m.Email

            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        

        #region 验证数据

        #endregion

        #region 功能方法
        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<UserDto>))] ICollection<UserDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            //初始化密码
            foreach (var userDto in dtos)
            {
                userDto.Password = "123";
            }
            OperationResult result = IdentityContract.AddUsers(dtos.ToArray());
           
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<UserDto>))] ICollection<UserDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = IdentityContract.EditUsers(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = IdentityContract.DeleteUsers(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoles()
        {
            var roles = IdentityContract.Roles.Select(r=>new
            {
                r.Id,
                r.Name
            });
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        [AjaxOnly]
        public ActionResult EditUser2Role(int userId, int[] selectId)
        {
            OperationResult result = IdentityContract.SetUserRoles(userId, selectId);
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }
        #endregion


       

        #region 视图功能

        public ActionResult User2Role(int? id)
        {
            var firstOrDefault = IdentityContract.Users.FirstOrDefault(u => u.Id == id);
            if (firstOrDefault != null)
            {
                var data = firstOrDefault.Roles.Select(r => r.Id);
                ViewBag.SelectedId = string.Join(",", data);
            }
            return View();
        }
        #endregion
    }
}