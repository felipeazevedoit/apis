using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    public class SharedController : Controller
    {
        [ChildActionOnly]
        [ActionName("GetHeader")]
        public ActionResult GetHeaderAsync()
        {
            return PartialView("_Header");
        }

        public ActionResult _Layout()
        {
            return PartialView();
        }
    }
}