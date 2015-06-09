

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mes.Demo.Contracts;
using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Demo.Web.ViewModels;
using Mes.Utility;
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
                return RedirectToAction("Login");
            }
            List<Menu> menus = new List<Menu>();
            foreach (var role in user2.Roles.ToList())
            {
                menus.AddRange(role.Menus); 
            }
            List<TreeNode> treeNode0 = new List<TreeNode>();
            foreach (var menu in menus.Where(m=>m.TreePath=="1").Distinct().OrderBy(m=>m.SortCode))
            {
                TreeNode treeNode1 = new TreeNode();
                treeNode1.Id = menu.Id.ToString();
                treeNode1.Text = menu.Remark;
                treeNode1.IconCls = "pic_26";
                List<TreeNode> children= new List<TreeNode>();
                foreach (var child in menu.Children)
                {
                    if (menus.Contains(child))
                    children.Add(new TreeNode() {Id =child.Id.ToString(), Text = child.Remark, IconCls = "pic_93", Url = Url.Action("Index", child.Name) });
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

        #endregion

        #endregion

        #region 视图功能

        #endregion

        public ActionResult Index()
        {
            Logger.Debug("访问后台管理首页");
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }
        

        public ActionResult Login(UserDto userDto)
        {
            
            var user = IdentityContract.Users.FirstOrDefault(u => u.Name == userDto.Name && u.Password == userDto.Password);
            if (user == null)
                return View();
            Session.Add("user",user);
            return RedirectToAction("Index");
        }
      
    }
}