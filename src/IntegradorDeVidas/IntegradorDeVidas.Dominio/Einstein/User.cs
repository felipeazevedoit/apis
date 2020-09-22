using System;
using System.Collections.Generic;
using System.Text;

namespace IntegradorDeVidas.Dominio.Einstein
{
    public class User
    {
        public bool primeiro_acesso { get; set; }
        public List<Product> products { get; set; }
        public List<Platform> platforms { get; set; }
        public int contadorSenhasIncorretas { get; set; }
        public bool status { get; set; }
        public string _id { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public IdPerfil id_perfil { get; set; }
        public string cpf { get; set; }
        public string codigo_classe { get; set; }
        public string telefone { get; set; }
        public string assinatura { get; set; }
        public string convenio { get; set; }
        public string id_convenio { get; set; }
        public string senha { get; set; }
        public DateTime createdAt { get; set; }
    }
}
