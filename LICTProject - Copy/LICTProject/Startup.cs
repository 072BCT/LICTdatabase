using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LICTProject.Startup))]
namespace LICTProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
