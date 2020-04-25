using System.Web.Mvc;

namespace WebSchools.Areas.JavaScript
{
    public class JavaScriptAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JavaScript";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JavaScript_default",
                "JavaScript/{controller}/{action}/{id}",
                new { controller="JavaScriptTutorial", action = "Js", id = UrlParameter.Optional }
            );
        }
    }
}