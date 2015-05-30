

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        public ActionResult GridData()
        {
            List<object> data = new List<object>();
            for (int i = 1; i <= 20; i++)
            {
                var item = new { Id = i, Name = "UserName" + i, NickName = "用户" + i, IsDeleted = false, CreatedTime = DateTime.Now.AddMinutes(i) };
                data.Add(item);
            }
            return Json(new GridData<object>(data, data.Count), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 验证数据

        #endregion

        #region 功能方法

        #endregion

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}