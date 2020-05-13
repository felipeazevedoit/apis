using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebPixCoreIn
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            var current = HttpContext.Current;
            string url = HttpContext.Current.Request.Url.Host;
            int porta = HttpContext.Current.Request.Url.Port;
            string protocolo = HttpContext.Current.Request.Url.Scheme;
            var urlDoCliente = "";

            if (porta != 80)
                urlDoCliente = protocolo + "://" + url + ":" + porta.ToString() + "/";
            else
                urlDoCliente = protocolo + "://" + url + "/";

            if (current.Request.Cookies["UsuarioLogado"] == null)
            {
                if (!current.Request.Url.AbsoluteUri.Contains("login"))
                {
                    current.Response.Redirect(urlDoCliente + "login/login");
                }
            }
        }
    }
}
