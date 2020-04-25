using System.Web.Mvc;

namespace WebSchools.Areas.Ict
{
    public class IctAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ict";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ict_default",
                "Ict/{controller}/{action}/{id}",
                new { controller="IctTutorial", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}