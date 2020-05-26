using Newtonsoft.Json;
using SaudeComVoce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SaudeComVc_Home
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
            var url = HttpContext.Current.Request.Url.Host;
            var porta = HttpContext.Current.Request.Url.Port;
            var protocolo = HttpContext.Current.Request.Url.Scheme;

            var urlDoCliente = string.Empty;

            if (porta != 80)
                urlDoCliente = protocolo + "://" + url + ":" + porta.ToString() + "/";
            else
                urlDoCliente = protocolo + "://" + url + "/";

            int idCliente = PixCoreValues.VerificaUrlCliente(urlDoCliente);
            if (idCliente != 0)
            {
                //var cookievalue = string.Empty;
                //if (Request.Cookies["IdClienteSaudeSite"] != null)
                //{
                //    cookievalue = Request.Cookies["IdClienteSaudeSite"].Value.ToString();
                //}
                //else
                //{
                Response.Cookies["IdClienteSaudeSite"].Value = idCliente.ToString();
                Response.Cookies["IdClienteSaudeSite"].Expires = DateTime.Now.AddMinutes(1); // add expiry time

                //HttpCookie cookie = new HttpCookie("UsuarioLogado");
                //cookie.Value = "0";
                //cookie.Expires = DateTime.Now.AddMinutes(120);

                //Response.Cookies.Set(cookie);
                //}

                //PixCoreValues.RenderUrlPage(HttpContext.Current);
            }
            else
            {
                Response.StatusCode = 404;
            }

            //var usuariologado = PixCoreValues.UsuarioLogado;
            //if (usuariologado == null || usuariologado.IdUsuario == 0)
            //{
            //    if (!HttpContext.Current.Request.Url.AbsoluteUri.Contains("Home"))
            //    {
            //        if (usuariologado == null || usuariologado.IdUsuario == 0)
            //        {
            //            HttpContext.Current.Response.Redirect(urlDoCliente + "Home/Index");
            //        }
            //        else
            //        {
            //            HttpContext.Current.Response.Redirect(urlDoCliente);
            //        }
            //    }
            //}
        }
    }
}
