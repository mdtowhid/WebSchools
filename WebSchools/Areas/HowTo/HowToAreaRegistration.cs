using System.Web.Mvc;

namespace WebSchools.Areas.HowTo
{
    public class HowToAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HowTo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HowTo_default",
                "HowTo/{controller}/{action}/{id}",
                new {controller="HowTo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}