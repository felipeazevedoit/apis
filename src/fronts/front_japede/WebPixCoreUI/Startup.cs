using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPixCoreUI.Startup))]
namespace WebPixCoreUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
