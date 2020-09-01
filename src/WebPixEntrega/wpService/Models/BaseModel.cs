using System;

namespace wpService.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataEdicao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public bool Ativo { get; set; }
        public int Status { get; set; }
        public int idCliente { get; set; }
    }
}
