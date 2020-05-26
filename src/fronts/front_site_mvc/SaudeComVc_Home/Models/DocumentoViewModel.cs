using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class DocumentoViewModel : BaseModel
    {
        public byte[] Arquivo { get; set; }
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public bool Requerido { get; set; }
        public int CodigoExterno { get; set; }
        public int DocumentoStatusID { get; set; }
        public int DocStatusObsID { get; set; }

        public DocumentoTipo DocumentoTipo { get; set; }
        public DocumentoStatus DocumentoStatus { get; set; }
        public DocStatusObservacoes StatusObservacoes { get; set; }

        [JsonIgnore]
        public byte[] ArquivoB { get; set; }

        public DocumentoViewModel()
        {

        }

        public DocumentoViewModel(int ID, string nome, string descricao, DateTime dataCriacao, DateTime dateAlteracao, int usuarioCriacao, int usuarioEdicao, int status, int idCliente,
            byte[] arquivo, string numero, int tipo, bool requerido, int codigoExterno, int statusId)
            : base(nome, descricao, dataCriacao, dateAlteracao, usuarioCriacao, usuarioEdicao, status, idCliente, true)
        {
            Arquivo = arquivo;
            Numero = numero;
            Tipo = tipo;
            Requerido = requerido;
            CodigoExterno = codigoExterno;
            DocumentoStatusID = statusId;
        }
    }
}