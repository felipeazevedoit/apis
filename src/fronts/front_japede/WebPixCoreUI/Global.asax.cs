﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebPixCoreUI.Models;

namespace WebPixCoreUI
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

            string url = HttpContext.Current.Request.Url.Host;
            int porta = HttpContext.Current.Request.Url.Port;
            string protocolo = HttpContext.Current.Request.Url.Scheme;
            var urlDoCliente = "";

            if (porta != 80)
                urlDoCliente = protocolo + "://" + url + ":" + porta.ToString() + "/";
            else
                urlDoCliente = protocolo + "://" + url + "/";

            int idCliente = PixCore.PixCore.VerificaUrlCliente(urlDoCliente);
            if (idCliente != 0)
            {
                string cookievalue;
                if (Request.Cookies["IdCliente"] != null)
                {
                    cookievalue = Request.Cookies["IdCliente"].ToString();
                }
                else
                {
                    Response.Cookies["IdCliente"].Value = idCliente.ToString();
                    Response.Cookies["IdCliente"].Expires = DateTime.Now.AddMinutes(30); // add expiry time
                }
                PixCore.PixCore.RenderUrlPage(HttpContext.Current);
            }
            else
            {
                Response.StatusCode = 404;
            }


        }
    }
}
