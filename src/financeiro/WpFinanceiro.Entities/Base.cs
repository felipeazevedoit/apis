using System;
using System.Collections.Generic;
using System.Text;

namespace WpFinanceiro.Entities
{
    public class Base
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public bool Ativo { get; set; }
        public int IdCliente { get; set; }

        public IDictionary<string, object> GetBasePorperties()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            if (ID > 0)
                result.Add(nameof(ID), ID);

            if (!string.IsNullOrEmpty(Nome))
                result.Add(nameof(Nome), Nome);

            if (!string.IsNullOrEmpty(Descricao))
                result.Add(nameof(Descricao), Descricao);

            if (DataCriacao != null && DataCriacao > DateTime.MinValue)
                result.Add(nameof(DataCriacao), DataCriacao);

            if (DataEdicao != null && DataEdicao > DateTime.MinValue)
                result.Add(nameof(DataEdicao), DataEdicao);

            if (UsuarioCriacao > 0)
                result.Add(nameof(UsuarioCriacao), UsuarioCriacao);

            if (UsuarioEdicao > 0)
                result.Add(nameof(UsuarioEdicao), UsuarioEdicao);

            if (Ativo)
                result.Add(nameof(Ativo), Ativo);

            if (IdCliente > 0)
                result.Add(nameof(IdCliente), IdCliente);

            return result;
        }
    }
}
