using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPixCoreUI.Models;


namespace WebPixCoreUI.Controllers
{
    public class PageController : Controller
    {

        private int IDCliente = PixCore.PixCore.IDCliente;
       
        // GET: Page
        public ActionResult Index(int id)
        {
            int idUsuario = 0;

            if (PixCore.PixCore.UsuarioLogado.IdUsuario == 0)
                idUsuario = 999;
            else
                idUsuario = PixCore.PixCore.UsuarioLogado.IdUsuario;

            ViewBag.urlCliente = PixCore.PixCore.defaultSiteUrl;
            ViewBag.NomeCliente = PixCore.PixCore.Cliente.Nome;


            var keyUrlIn = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var urlAPIIn = keyUrlIn + "Seguranca/Principal/buscarpaginas/" + IDCliente + "/" + idUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(urlAPIIn));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            PageViewModel[] Pagina = jss.Deserialize<PageViewModel[]>(result);

            var modelo = Pagina.Where(x => x.ID == id).FirstOrDefault();
            var base64EncodedBytes = System.Convert.FromBase64String(modelo.Conteudo);
            string converted = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            modelo.Conteudo = converted;
            ViewBag.Title = modelo.Titulo;

           return View(modelo);
        }
        public ContentResult GetCss()
        {

            int idUsuario = 0;

            if (PixCore.PixCore.UsuarioLogado.IdUsuario == 0)
                idUsuario = 999;
            else
                idUsuario = PixCore.PixCore.UsuarioLogado.IdUsuario;

            var keyUrlIn = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var urlAPIIn = keyUrlIn + "Seguranca/Principal/buscarEstilo/" + IDCliente + "/" +  idUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(urlAPIIn));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            EstiloViewModel[] Estilos = jss.Deserialize<EstiloViewModel[]>(result);

            var resultado = Estilos.Where(x => x.idCliente == IDCliente).FirstOrDefault();
            string cssBody = "";

            if (resultado != null)
                cssBody = resultado.Conteudo;
           
            return Content(cssBody, "text/css");
        }
    }
}