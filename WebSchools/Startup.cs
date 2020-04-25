using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSchools.Startup))]
namespace WebSchools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
