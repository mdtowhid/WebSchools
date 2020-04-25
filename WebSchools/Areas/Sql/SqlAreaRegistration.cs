using System.Web.Mvc;

namespace WebSchools.Areas.Sql
{
    public class SqlAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sql";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sql_default",
                "Sql/{controller}/{action}/{id}",
                new { controller="SqlTutorial", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}