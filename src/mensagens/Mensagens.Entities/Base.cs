using System;
using System.Collections.Generic;
using System.Text;

namespace Mensagens.Entities
{ 
    public abstract class Base
    {
        public Base()
        {

        }
        
        protected Base(string nome, string descricao, int usuarioCriacao, int usuarioEdicao, int idCliente)
        {
            Nome = nome;
            Descricao = descricao;
            UsuarioCriacao = usuarioCriacao;
            UsuarioEdicao = usuarioEdicao;
            IdCliente = idCliente;
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DateAlteracao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public bool Ativo { get; set; }
        public int Status { get; set; }
        public int IdCliente { get; set; }
    }
}
