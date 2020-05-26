using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class TermosController : Controller
    {
        // GET: Termos
        public ActionResult Index()
        {
            return View();
        }
    }
}