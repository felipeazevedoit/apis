using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPixCoreUI.Models;
using WebPixCoreUI.PixCore;

namespace WebPixCoreUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login/Create
        public ActionResult Login()
        {
            ViewBag.urlCliente = PixCore.PixCore.defaultSiteUrl;
            ViewBag.NomeCliente = PixCore.PixCore.Cliente.Nome;
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Login(LoginViewModel collection)
        {
            try
            {
                if (PixCore.PixCore.Login(collection))
                {
                    Response.Redirect(PixCore.PixCore.defaultSiteUrl);
                    return View();
                }
                else
                {
                    ViewBag.TemporariaMensagem = "Usuario ou senha invalida";
                    return View();
                }
            }
            catch
            {
                ViewBag.TemporariaMensagem = "Usuario ou senha invalida";
                return View();
            }
        }
    }
}
