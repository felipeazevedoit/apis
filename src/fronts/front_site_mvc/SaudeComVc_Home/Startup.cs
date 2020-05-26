using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SaudeComVc_Home.Startup))]

namespace SaudeComVc_Home
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie"
                ,LoginPath = new PathString("/Login")
                ,LogoutPath = new PathString("/")
            });
        }
    }
}
