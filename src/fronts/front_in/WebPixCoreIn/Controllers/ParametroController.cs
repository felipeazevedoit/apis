using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPixCoreIn.Models;

namespace WebPixCoreIn.Controllers
{
    public class ParametroController : Controller
    {
        private WebPixCoreInContext db = new WebPixCoreInContext();

        // GET: Parametro
        public ActionResult Index()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Parametro";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ParametroViewModel[] Parametro = jss.Deserialize<ParametroViewModel[]>(result);

            var ParametroFiltrado = Parametro.ToList();

            return View(ParametroFiltrado);
        }

        // GET: Parametro/Details/5
        public ActionResult Details(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Parametro";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ParametroViewModel[] Parametro = jss.Deserialize<ParametroViewModel[]>(result);

            var ParametroFiltrado = Parametro.Where(x => x.ID == id).ToList();

            return View(ParametroFiltrado);
        }

        // GET: Parametro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parametro/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo,idAcao,Ordem,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] ParametroViewModel parametroViewModel)
        {
            if (ModelState.IsValid)
            {
                parametroViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                parametroViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                parametroViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Parametro";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = parametroViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(parametroViewModel);
        }

        // GET: Parametro/Edit/5
        public ActionResult Edit(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Parametro";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ParametroViewModel[] Parametro = jss.Deserialize<ParametroViewModel[]>(result);

            var ParametroFiltrado = Parametro.Where(x => x.ID == id).ToList();

            return View(ParametroFiltrado);

        }

        // POST: Parametro/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tipo,idAcao,Ordem,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] ParametroViewModel parametroViewModel)
        {
            if (ModelState.IsValid)
            {
                parametroViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                parametroViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                parametroViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Parametro";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = parametroViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(parametroViewModel);
        }  

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
