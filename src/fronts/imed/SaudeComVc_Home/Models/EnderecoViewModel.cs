using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class EnderecoViewModel : BaseModel
    {
        public EnderecoViewModel(string nome, string descricao, DateTime dataCriacao, DateTime dateAlteracao, int usuarioCriacao,
            int usuarioEdicao, int status, int idCliente, bool ativo, string cEP, string estado, string cidade, string bairro, string local, int numeroLocal, string complemento, int idUsuario, string uf) : base(nome, descricao, dataCriacao, dateAlteracao, usuarioCriacao, usuarioEdicao, status, idCliente, ativo)
        {
            CEP = cEP;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Local = local;
            NumeroLocal = numeroLocal;
            Complemento = complemento;
            IdUsuario = idUsuario;
            Uf = uf;
        }

        public EnderecoViewModel()
        {

        }

        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Local { get; set; }
        public int NumeroLocal { get; set; }
        public string Complemento { get; set; }
        public int IdUsuario { get; set; }
        public string Uf { get; set; }

        public string Endereco { get; set; }
    }
}