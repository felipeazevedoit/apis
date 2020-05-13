using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpFinanceiro.Entities
{
    public class Extrato : Base
    {
        public decimal Valor { get; set; }
        public int NaturezaId { get; set; }
        public int TipoId { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int? CodigoExterno { get; set; }
        public Status StatusId { get; set; }
        public int TipoDestino { get; set; }
        public int TipoOrigem { get; set; }

        [NotMapped]
        public DateTime DataInicio { get; set; }
        [NotMapped]
        public DateTime DataFim { get; set; }
        [NotMapped]
        public string EmailEmpresa { get; set; }

        public IDictionary<string, object> GetProperties()
        {
            var result = base.GetBasePorperties();

            if (Valor != 0)
                result.Add(nameof(Valor), Valor);

            if (NaturezaId > 0)
                result.Add(nameof(NaturezaId), NaturezaId);

            if (TipoId > 0)
                result.Add(nameof(TipoId), TipoId);

            if (!string.IsNullOrEmpty(Origem))
                result.Add(nameof(Origem), Origem);

            if (!string.IsNullOrEmpty(Destino))
                result.Add(nameof(Destino), Destino);

            if (CodigoExterno > 0)
                result.Add(nameof(CodigoExterno), CodigoExterno);

            if (StatusId > 0)
                result.Add(nameof(StatusId), StatusId);

            if (TipoOrigem > 0)
                result.Add(nameof(TipoOrigem), TipoOrigem);

            if (TipoDestino > 0)
                result.Add(nameof(TipoDestino), TipoDestino);

            return result;
        }
    }
}
