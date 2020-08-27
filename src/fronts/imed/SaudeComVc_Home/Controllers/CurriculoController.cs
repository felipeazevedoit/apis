using Newtonsoft.Json;
using SaudeComVc_Home.Exceptions;
using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class CurriculoController : Controller
    {
        private readonly HttpClient _client;

        public CurriculoController()
        {
            _client = new HttpClient();
        }

        // GET: Curriculo
        public ActionResult Index()
        {
            return View();
        }

        public static byte[] StringParaByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        [ValidateInput(false)]
        public async Task<DocumentoViewModel> SaveDocumentoAsync(string base64, DocumentoViewModel documento)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var usuario = PixCoreValues.UsuarioLogado;

                var uri = new Uri($"{keyUrl}/Seguranca/wpDocumento/SalvarDocumento/{ usuario.idCliente }/{ usuario.IdUsuario }");

                byte[] arquivo = Convert.FromBase64String(base64);

                var curriculo = await BuscarCurriculoAsync(usuario.IdUsuario);

                if (curriculo != null)
                {
                    for (int i = 0; i < curriculo.Count(); i++)
                    {
                        documento.ID = curriculo.ElementAtOrDefault(i).ID;
                    }
                }

                Random randNum = new Random();

                documento.Arquivo = arquivo;
                documento.Ativo = true;
                documento.CodigoExterno = usuario.IdUsuario;
                documento.DataCriacao = DateTime.Parse(DateTime.Now.ToShortDateString(), CultureInfo.GetCultureInfo("pt-BR"));
                documento.DateAlteracao = DateTime.Parse(DateTime.Now.ToShortDateString(), CultureInfo.GetCultureInfo("pt-BR"));
                documento.DocStatusObsID = 0;
                documento.DocumentoStatusID = 2;
                documento.IdCliente = usuario.idCliente;
                documento.Nome = "Curriculo";
                documento.Descricao = "Curriculo - " + usuario.Nome;
                documento.Numero = Convert.ToString(randNum.Next());
                documento.Requerido = true;
                documento.Status = 1;
                documento.Tipo = 5;
                documento.UsuarioCriacao = usuario.IdUsuario;
                documento.UsuarioEdicao = usuario.IdUsuario;

                var obj = new
                {
                    documento,
                };

                #region app
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var doc = JsonConvert.DeserializeObject<object>(result);

                    if (!(doc is bool))
                    {
                        var savedDoc = JsonConvert.DeserializeObject<DocumentoViewModel>(Convert.ToString(doc));

                        if (savedDoc != null && savedDoc.ID > 0)
                        {
                            return savedDoc;
                        }
                        else throw new DocumentoException("Não foi possível salvar o documento selecionado.");
                    }
                    else
                    {
                        throw new DocumentoException("Número do documento informado já foi cadastrado no sistema.");
                    }
                }
                else
                {
                    throw new DocumentoException("Verifique os dados informados.");
                }
                #endregion
            }
            catch (DocumentoException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new DocumentoException("Não foi possível salvar o documento informado", e);
            }
        }

        public async Task<IEnumerable<DocumentoViewModel>> BuscarCurriculoAsync(int codExterno)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                if (usuario.IdUsuario > 0 && usuario.idPerfil == 12)
                {
                    codExterno = usuario.IdUsuario;
                }

                var envio = new
                {
                    codigoExterno = codExterno,
                    idCliente = 12
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<DocumentoViewModel>>(keyUrl,
                    $"/Seguranca/wpDocumento/BuscarPorCodigo/{ 12 }/{ 999 }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os convênios.", e);
            }
        }

        public async Task<ActionResult> Gerar(int codExterno)
        {
            
            var result = await BuscarCurriculoAsync(codExterno);

            var curriculo = new DocumentoViewModel();

            for (int i = 0; i < result.Count(); i++)
            {
                curriculo.ArquivoB = result.ElementAtOrDefault(i).Arquivo;
            }
            if (result != null)
            {
                Response.Clear();
                MemoryStream ms = new MemoryStream(curriculo.ArquivoB);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=curriculo.pdf");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
            return View("WhiteLabel", "Medico");
        }
    }
}