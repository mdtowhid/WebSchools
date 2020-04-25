using System.Web.Mvc;

namespace WebSchools.Areas.Content
{
    public class ContentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Content";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Content_default",
                "Content/{controller}/{action}/{id}",
                new { controller = "Content", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}