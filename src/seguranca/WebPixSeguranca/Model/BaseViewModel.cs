using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixSeguranca.Model
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DateAlteracao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; }
        public int idCliente { get; set; }
    }
}
