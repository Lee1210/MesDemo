using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web.Mvc;

using Mes.Demo.Contracts;
using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Demo.Web.ViewModels;
using Mes.Utility;
using Mes.Utility.Data;
using Mes.Utility.Logging;
using Mes.Web.Mvc.Security;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(HomeController));
        public IIdentityContract IdentityContract { get; set; }

        #region Ajax功能

        #region 获取数据

        [AjaxOnly]
        public ActionResult GetMenuData()
        {
            User user = (User)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            User user2 = IdentityContract.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
            if (user2 == null)
            {
                // return Content("用户不存在");
                return RedirectToAction("Login");
            }
            List<Menu> menus = new List<Menu>();
            foreach (var role in user2.Roles.ToList())
            {
                menus.AddRange(role.Menus);
            }
            List<TreeNode> treeNode0 = new List<TreeNode>();
            foreach (var menu in menus.Where(m => m.TreePath == "1").Distinct().OrderBy(m => m.SortCode))
            {
                TreeNode treeNode1 = new TreeNode();
                treeNode1.Id = menu.Id.ToString();
                treeNode1.Text = menu.Remark;
                treeNode1.IconCls = "pic_26";
                List<TreeNode> children = new List<TreeNode>();
                foreach (var child in menu.Children)
                {
                    if (menus.Contains(child))
                        children.Add(new TreeNode() { Id = child.Id.ToString(), Text = child.Remark, IconCls = "pic_93", Url = Url.Action(child.ActionName, child.Name) });
                }
                treeNode1.Children = children;
                treeNode0.Add(treeNode1);
            }

            return Json(treeNode0, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 验证数据

        #endregion

        #region 功能方法
        public ActionResult Login(UserDto userDto)
        {
            userDto.Name.CheckNotNullOrEmpty("Name");

            OperationResult operationResult = IdentityContract.Login(userDto.Name,userDto.Password);
            if (operationResult.ResultType == OperationResultType.Error)
                return Content("<script>location.href='/Home/Index';alert('" + operationResult.Message + "');</script>");
            Session.Add("user", operationResult.Data);
            return View("Main");
        }

        #endregion

        #endregion

        #region 视图功能
        public ActionResult Index()
        {
            Logger.Debug("访问后台管理首页");
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        #endregion

    }
}