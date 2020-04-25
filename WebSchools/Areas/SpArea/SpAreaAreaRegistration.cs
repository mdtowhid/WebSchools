using System.Web.Mvc;

namespace WebSchools.Areas.SpArea
{
    public class SpAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SpArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SpArea_default",
                "SpArea/{controller}/{action}/{id}",
                new {controller="T", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}