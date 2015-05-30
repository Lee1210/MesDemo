using System.Web.Mvc;

using Mes.Web.Mvc.Routing;


namespace Mes.Demo.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapLowerCaseUrlRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Mes.Demo.Web.Areas.Admin.Controllers" }
                );
        }
    }
}