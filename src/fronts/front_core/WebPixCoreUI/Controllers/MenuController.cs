using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebPixCoreUI.Controllers
{
    public class MenuController : Controller
    {
        // GET: api/Menu
        public JsonResult GetMenu()
        {
            var keyUrlIn = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var urlAPIIn = keyUrlIn + "menu";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(urlAPIIn));

            return Json(result);
        }

        // GET: api/Menu/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Menu
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Menu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Menu/5
        public void Delete(int id)
        {
        }
    }
}
