using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mes.Web.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ActionAuthorityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { //在Action执行前执行 TimeOut ActionAuthority
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["user"] == null)
            {
              //  var data = new { Type = "ActionAuthority", Content = "不具有当前操作的权限!" };
                var data = new { Type = "TimeOut", Content = "超时请重新登陆!" };
                filterContext.Result = new JsonResult { Data= data };//功能权限弹出提示框

                //filterContext.HttpContext.Response.Redirect("/admin/Home/Index"); 
            }
            //filterContext.HttpContext.Response.Redirect("dms/add");

            //base.OnActionExecuting(filterContext);
        }
    }
}
