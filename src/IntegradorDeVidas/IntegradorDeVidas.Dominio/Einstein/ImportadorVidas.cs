using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegradorDeVidas.Dominio.Einstein
{
    public class ImportadorVidas
    {
        public string Type { private get; set; }
        public string Postback { private get; set; }
        public string Operacao { private get; set; }
        public List<Vida> Dados {  get; set; }

        public ImportadorVidas()
        {
            SetValuesThis();
            Dados = new List<Vida>();
        }

        public ImportadorVidas(List<Vida> vidas)
        {
            Dados = vidas;
            SetValuesThis();
        }

        private void SetValuesThis()
        {
            Type = "text";
            Postback = "https://minhaapidepostback.com.br/postback";
            Operacao = "A";
        }

        public ImportadorVidas(Vida vida)
        {
            SetValuesThis();
            Dados = new List<Vida>
            {
                vida
            };
        }

        public List<string> ValidarListaDeVidas()
        {
            var erros = new List<string>();
            if (Dados.Count() < 1)
                erros.Add("É preciso que haja ao menos um registro para esta operação.");

            if (Dados.Count() >= 100)
                erros.Add("Só permitido inserir 100 itens por requisição");

            return erros;

        }
    }
}
