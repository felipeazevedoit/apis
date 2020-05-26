using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SaudeComVoce.Models
{
    public class BaseModel
    {
        public BaseModel()
        {

        }

        public BaseModel(string nome, string descricao, DateTime dataCriacao, DateTime dateAlteracao, int usuarioCriacao, int usuarioEdicao, int status, int idCliente, bool ativo)
        {
            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            DateAlteracao = dateAlteracao;
            UsuarioCriacao = usuarioCriacao;
            UsuarioEdicao = usuarioEdicao;
            Status = status;
            IdCliente = idCliente;
            Ativo = ativo;
        }

        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        
        public DateTime DateAlteracao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public int Status { get; set; }
        public int IdCliente { get; set; }
        public bool Ativo { get; set; }

        private DateTime _dataCriacao;
        public DateTime DataCriacao
        {
            get => _dataCriacao;
            set
            {
                _dataCriacao = value;
                _comentarioHora = value.TimeOfDay.ToString(@"hh\:mm");
            }
        }

        private string _comentarioHora;
        

        [JsonIgnore]
        public string ComentarioHora
        {
            get => _comentarioHora;
            set => _comentarioHora = value;
        }
    }
}