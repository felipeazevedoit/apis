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
    public class AcaoController : Controller
    {
        private WebPixCoreInContext db = new WebPixCoreInContext();

        // GET: Acao
        public ActionResult Index()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AcaoViewModel[] acao = jss.Deserialize<AcaoViewModel[]>(result);

            var acaoFiltrado = acao.ToList();

            return View(acaoFiltrado);
        }

        // GET: Acao/Details/5
        public ActionResult Details(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AcaoViewModel[] acao = jss.Deserialize<AcaoViewModel[]>(result);

            var acaoFiltrado = acao.Where(x=> x.ID == id).FirstOrDefault();

            return View(acaoFiltrado);
        }

        // GET: Acao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,idTipoAcao,Caminho,idMotorAux,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] AcaoViewModel acaoViewModel)
        {
            if (ModelState.IsValid)
            {
                acaoViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                acaoViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                acaoViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Cliente";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = acaoViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }



            return View(acaoViewModel);
        }

        // GET: Acao/Edit/5
        public ActionResult Edit(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AcaoViewModel[] acao = jss.Deserialize<AcaoViewModel[]>(result);

            var acaoFiltrado = acao.Where(x => x.ID == id).FirstOrDefault();

            return View(acaoFiltrado);
        }

        // POST: Acao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,idTipoAcao,Caminho,idMotorAux,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] AcaoViewModel acaoViewModel)
        {
            if (ModelState.IsValid)
            {
                acaoViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                acaoViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                acaoViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Cliente";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = acaoViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }
            
            return View(acaoViewModel);
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
