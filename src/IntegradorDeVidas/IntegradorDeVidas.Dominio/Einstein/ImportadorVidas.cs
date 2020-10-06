using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegradorDeVidas.Dominio.Einstein
{
    public class ImportadorVidas
    {
        public string Type {  get; set; }
        public string Postback {  get; set; }
        public string Operacao {  get; set; }
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
            Type = "text";
            Postback = "https://minhaapidepostback.com.br/postback";
            Operacao = "A";
            Dados = new List<Vida>
            {
                vida
            };
        }

        public List<string> ValidarListaDeVidas()
        {
            var erros = new List<string>();
            if (Dados.Count() < 1)
                erros.Add("É preciso que haja ao menos um registro de vida para esta operação.");
            else
            {
                foreach (var item in Dados)
                {
                    if(item.DNAS == null)
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
            if (Dados.Count() >= 100)
                erros.Add("Só permitido inserir 100 itens por requisição");

           


            return erros;

        }
    }
}
