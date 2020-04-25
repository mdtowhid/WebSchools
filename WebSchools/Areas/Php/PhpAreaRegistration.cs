using System.Web.Mvc;

namespace WebSchools.Areas.Php
{
    public class PhpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Php";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Php_default",
                "Php/{controller}/{action}/{id}",
                new { controller="PhpTutorial",action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}