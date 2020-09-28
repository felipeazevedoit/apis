using System;
using System.Collections.Generic;
using System.Text;

namespace WpCotacao.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public bool PoliticamenteExposta { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Complemento{ get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public DateTime dataAlteracao { get; set; }
        public int Status { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idCliente { get; set; }


    }
}
