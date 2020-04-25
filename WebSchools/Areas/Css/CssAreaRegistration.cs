using System.Web.Mvc;

namespace WebSchools.Areas.Css
{
    public class CssAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Css";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Css_default",
                "Css/{controller}/{action}/{id}",
                new { controller = "CssTutorial", action = "Index", id = "css_default" }
            );
        }
    }
}