using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegradorDeVidas.Dominio.Einstein
{
    public class ImportadorVidas
    {
        public string type { get; set; }
        public string postback { get; set; }
        public string operacao { get; set; }
        public List<Vida> dados { get; set; }

        public ImportadorVidas()
        {
            SetValuesThis();
            dados = new List<Vida>();
        }

        public ImportadorVidas(List<Vida> vidas)
        {
            dados = vidas;
            SetValuesThis();
        }

        private void SetValuesThis()
        {
            type = "ext";
            postback = "https://minhaapidepostback.com.br/postback";
            operacao = "A";
        }

        public ImportadorVidas(Vida vida)
        {
            SetValuesThis();
            dados = new List<Vida> { vida };
        }

        public List<string> ValidarListaDeVidas()
        {
            var erros = new List<string>();
            if (dados.Count() < 1)
                erros.Add("É preciso que haja ao menos um registro de vida para esta operação.");
            else
            {
                foreach (var item in dados)
                {
                    if (item.DNAS == null)
                        erros.Add("DNAS - Não pode ser nulo");
                    if (item.EMAIL == null)
                        erros.Add("EMAIL - Não pode ser nulo");
                    if (item.FONE == null)
                        erros.Add("FONE - Não pode ser nulo");
                    if (item.ID_EMPRESA == null)
                        erros.Add("ID_EMPRESA - Não pode ser nulo");
                    if (item.NCPF == null)
                        erros.Add("NCPF - Não pode ser nulo");
                    if (item.NCPFTITULAR == null)
                        erros.Add("NCPFTITULAR - Não pode ser nulo");
                    if (item.NOME == null)
                        erros.Add("NOME - Não pode ser nulo");
                    if (item.PAREN == null)
                        erros.Add("PAREN - Não pode ser nulo");
                    if (item.PRODUTO == null)
                        erros.Add("PRODUTO - Não pode ser nulo");
                    if (item.SEXO == null)
                        erros.Add("SEXO - Não pode ser nulo");
                    if (item.SUBGRUPO == null)
                        erros.Add("SUBGRUPO - Não pode ser nulo");
                    if (item.TITU == null)
                        erros.Add("TITU - Não pode ser nulo");
                    if (item.VAL_ATE == null)
                        erros.Add("VAL_ATE - Não pode ser nulo");
                    if (item.VAL_DE == null)
                        erros.Add("VAL_DE - Não pode ser nulo");
                }
            }
            if (dados.Count() >= 100)
                erros.Add("Só permitido inserir 100 itens por requisição");




            return erros;

        }
    }
}
