using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebPixCoreIn.Models;

namespace WebPixCoreIn.Controllers
{
    public class MotorAuxController : Controller
    {
        private WebPixCoreInContext db = new WebPixCoreInContext();

        // GET: MotorAux
        public ActionResult Index()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.ToList();

            return View(MotorAuxFiltrado);
        }

        // GET: MotorAux/Details/5
        public ActionResult Details(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.Where(x=> x.ID == id).FirstOrDefault();

            return View(MotorAuxFiltrado);
        }

        // GET: MotorAux/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalAcoes(int? id)
        {
            if (id != null)
            {
                var acoes = GetAcoesByMotor(Convert.ToInt32(id));
                var motor = GetMotorAuxilixarById(Convert.ToInt32(id));

                ViewBag.Motor = motor.Nome;
                ViewBag.IdMotor = motor.ID;
                foreach (var acao in acoes)
                {
                    acao.MotorAuxiliar = motor.Nome;
                    //acao.TipoAcao = tipoAcao.Nome;
                }

                return View(acoes);
            }

            return View();
        }

        private MotorAuxViewModel GetMotorAuxilixarById(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var motores = jss.Deserialize<IEnumerable<MotorAuxViewModel>>(result);

            var motor = motores.Where(m => m.ID.Equals(id)).SingleOrDefault();

            return motor;
        }

        private IEnumerable<MotorAuxViewModel> GetMotoresAuxiliares()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var motores = jss.Deserialize<IEnumerable<MotorAuxViewModel>>(result);

            return motores;
        }

        // POST: MotorAux/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Metodo,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] MotorAuxViewModel motorAuxViewModel)
        {
            if (ModelState.IsValid)
            {
                motorAuxViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "MotorAux/Save";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = motorAuxViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(motorAuxViewModel);
        }

        // GET: MotorAux/Edit/5
        public ActionResult Edit(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.Where(x => x.ID == id).FirstOrDefault();

            return View(MotorAuxFiltrado);
        }

        // POST: MotorAux/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Metodo,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] MotorAuxViewModel motorAuxViewModel)
        {
            if (ModelState.IsValid)
            {
                motorAuxViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "MotorAux/Save";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = motorAuxViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(motorAuxViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<AcaoViewModel> GetAcoesByMotor(int motorId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var resultAcoes = jss.Deserialize<AcaoViewModel[]>(result);

            var acoes = resultAcoes.Where(r => r.idMotorAux.Equals(motorId));
            return acoes;
        }

        private AcaoViewModel GetAcao(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var resultAcoes = jss.Deserialize<AcaoViewModel[]>(result);

            var acao = resultAcoes.Where(r => r.ID.Equals(id)).SingleOrDefault();
            return acao;
        }

        public ActionResult ModalAcao(int? id)
        {
            if (id != null)
            {
                var acao = GetAcao(Convert.ToInt32(id));
                var motores = GetMotoresAuxiliares();
                var motor = motores.FirstOrDefault(x => x.ID.Equals(acao.idMotorAux));

                ViewBag.Motor = motor.Nome;
                ViewBag.IdMotor = motor.ID;

                return View(acao);
            }

            return View(new AcaoViewModel());
        }

        public ActionResult SalvarAcao(int? motorId)
        {
            var motor = GetMotorAuxilixarById(Convert.ToInt32(motorId));

            return View("ModalAcao", new AcaoViewModel() { idMotorAux = motor.ID });
        }

        [HttpPost]
        public string SalvarAcao(AcaoViewModel acaoViewModel)
        {
            acaoViewModel.UsuarioCriacao = 1;
            acaoViewModel.UsuarioEdicao = 1;

            var result = string.Empty;
            using (var client = new WebClient())
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                var url = keyUrl + "Acao";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                var envio = acaoViewModel;
                var data = jss.Serialize(envio);
                result = client.UploadString(url, "POST", data);
            }

            return result;
        }

        [HttpGet]
        public ActionResult ModalParametros(int? id)
        {
            if (id != null)
            {
                var parametros = GetParametrosByAcao(Convert.ToInt32(id));
                var acao = GetAcao(Convert.ToInt32(id));

                ViewBag.Acao = acao.Nome;
                foreach (var parametro in parametros)
                {
                    parametro.Acao = acao.Nome;
                }

                return View(parametros);
            }

            return View();
        }

        private IEnumerable<ParametroViewModel> GetParametrosByAcao(int acaoId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Parametro";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var resultAcoes = jss.Deserialize<ParametroViewModel[]>(result);

            var acoes = resultAcoes.Where(r => r.idAcao.Equals(acaoId));
            return acoes;
        }

        public ActionResult ModalParametro(int? id)
        {
            if (id != null)
            {
                var parametro = GetParametro(Convert.ToInt32(id));

                var acao = GetAcao(Convert.ToInt32(parametro.idAcao));
                ViewBag.Acao = acao.Nome;

                return View(parametro);
            }

            return View(new ParametroViewModel());
        }

        private ParametroViewModel GetParametro(int parametroId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Parametro";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var resultParametros = jss.Deserialize<ParametroViewModel[]>(result);

            var parametro = resultParametros.Where(r => r.ID.Equals(parametroId)).SingleOrDefault();
            return parametro;
        }

        [HttpPost]
        public string SalvarParametro(ParametroViewModel parametroViewModel)
        {
            parametroViewModel.UsuarioCriacao = 1;
            parametroViewModel.UsuarioEdicao = 1;

            var result = string.Empty;
            using (var client = new WebClient())
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                var url = keyUrl + "Parametro";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                var envio = parametroViewModel;
                var data = jss.Serialize(envio);
                result = client.UploadString(url, "POST", data);
            }

            return result;
        }

        public ActionResult SalvarParametro(int? acaoId)
        {
            var acao = GetAcao(Convert.ToInt32(acaoId));
            ViewBag.Acao = acao.Nome;

            return View("ModalParametro", new ParametroViewModel() { idAcao = acao.ID });
        }
    }
}
