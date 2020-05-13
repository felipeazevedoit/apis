using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPixCoreIn.Models;

namespace WebPixCoreIn.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        // POST: Login/Create
        [HttpPost]
        public ActionResult login(LoginViewModel collection)
        {
            try
            {

                if(collection.login == "Killpdj")
                {
                    if(collection.senha == "lucas07")
                    {
                        // TODO: Add insert logic here
                        //Cria a estancia do obj HttpCookie passando o nome do mesmo

                        HttpCookie cookie = new HttpCookie("UsuarioLogado");

                        //Define o valor do cookie

                        cookie.Value = "Logado";


                        DateTime dtNow = DateTime.Now;

                        TimeSpan tsMinute = new TimeSpan(0, 0, 15, 0);

                        cookie.Expires = dtNow + tsMinute;

                        //Adiciona o cookie

                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Cliente");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }

                }
                else
                {

                    return RedirectToAction("Index", "Login");
                }

           
            }
            catch
            {
                return View();
            }
        }

    }
}
