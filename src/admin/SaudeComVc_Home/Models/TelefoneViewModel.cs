using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class TelefoneViewModel : BaseModel
    {
        public TelefoneViewModel(string numero)
        {
            Numero = numero;
        }

        public TelefoneViewModel()
        {

        }

        public TelefoneViewModel(string nome, string descricao, DateTime dataCriacao, DateTime dateAlteracao, 
            int usuarioCriacao, int usuarioEdicao, int status, int idCliente, bool ativo, string numero) : base(nome, descricao, dataCriacao, dateAlteracao, usuarioCriacao, usuarioEdicao, status, idCliente, ativo)
        {
            Numero = numero;
        }

        public string Numero { get; set; }
    }
}