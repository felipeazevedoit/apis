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
    public class ClienteController : Controller
    {
        private WebPixCoreInContext db = new WebPixCoreInContext();

        // GET: ClienteViewModels
        public ActionResult Index()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Cliente";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ClienteViewModel[] Cliente = jss.Deserialize<ClienteViewModel[]>(result);

            var ClienteFiltrado = Cliente.ToList();

            return View(ClienteFiltrado);
        }

        // GET: ClienteViewModels/Details/5
        public ActionResult Details(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Cliente";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ClienteViewModel[] Cliente = jss.Deserialize<ClienteViewModel[]>(result);

            var ClienteFiltrado = Cliente.Where(x=> x.ID == id).FirstOrDefault();

            return View(ClienteFiltrado);
        }

        // GET: ClienteViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteViewModels/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CNPJ,Email,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                clienteViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                clienteViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                clienteViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Cliente";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = clienteViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

           
            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Cliente";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ClienteViewModel[] Cliente = jss.Deserialize<ClienteViewModel[]>(result);

            var ClienteFiltrado = Cliente.Where(x => x.ID == id).FirstOrDefault();

            return View(ClienteFiltrado);
        }

        // POST: ClienteViewModels/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CNPJ,Email,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                clienteViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                clienteViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                clienteViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "Cliente";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = clienteViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
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
