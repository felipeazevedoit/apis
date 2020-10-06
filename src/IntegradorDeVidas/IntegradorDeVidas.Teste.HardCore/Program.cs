using IntegradorDeVidas.Dominio.Einstein;
using RestSharp;
using System;
using System.Collections.Generic;

namespace IntegradorDeVidas.Teste.HardCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var vida = new Vida
            {
                EMAIL = "teste@teste.com.br",
                FONE = "11 985896525",
                DNAS = "07/06/1975",
                NCPF = "58778545899",
                NOME = "José Teste",
                PRODUTO = "Einstein Conecta|Clínicas Einstein",
                ID_EMPRESA = "16ae230a-8acd-4fa0-88cd-1585717e3fac",
                SEXO = "M",
                TITU = "TITU",
                NCAR = "0000",
                PAREN = "15",
                SUBGRUPO = "GRUPO A",
                VAL_DE = "01/05/2020",
                VAL_ATE = "31/12/2020",
                NCPFTITULAR = "58778545899"
            };



            var root = new Servico.Einstein();
            root.CadastrarVidas(vida);


            //var client = new RestClient("https://portalempresas-backend-qas.telemedicinaeinstein.com.br/vidasElegiveis/cadastro")
            //{
            //    Timeout = -1
            //};
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjVmNDU3Yzg0ZWEyN2FkMDAxMjQxMjNmOSIsImlhdCI6MTYwMTk3ODcxMiwiZXhwIjoxNjAyMDY1MTEyfQ.m9h8TgUebmY1q61CMVx5MktkKYmTxzubE1DLn8NK_kc");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"type\": \"ext\",\n  \"postback\": \"https://minhaapidepostback.com.br/postback\",\n  \"operacao\": \"A\",\n  \"dados\": [\n    {\n      \"ID_EMPRESA\": \"16ae230a-8acd-4fa0-88cd-1585717e3fac\",\n      \"NOME\": \"Filipe Benedito das Neves\",\n      \"DNAS\": \"07/06/1975\",\n      \"NCPF\": \"548.916.428-07\",\n      \"SEXO\": \"M\",\n      \"TITU\": \"TITU\",\n      \"NCAR\": \"00000\",\n      \"PAREN\": \"15\",\n      \"SUBGRUPO\": \"GRUPO A\",\n      \"EMAIL\": \"email@email.com\",\n      \"FONE\": \"(11) 99999-9999\",\n      \"VAL_DE\": \"31/12/2019\",\n      \"VAL_ATE\": \"31/12/2019\",\n      \"PRODUTO\": \"Einstein Conecta|Clínicas Einstein\"\n    },\n    {\n      \"ID_EMPRESA\": \"16ae230a-8acd-4fa0-88cd-1585717e3fac\",\n      \"NOME\": \"Roberto Benedito das Neves\",\n      \"DNAS\": \"07/06/2015\",\n      \"SEXO\": \"M\",\n      \"NCPFTITULAR\": \"548.916.428-07\",\n      \"TITU\": \"DEPE\",\n      \"NCAR\": \"00000\",\n      \"PAREN\": \"16\",\n      \"SUBGRUPO\": \"GRUPO A\",\n      \"EMAIL\": \"email@email.com\",\n      \"FONE\": \"(11) 99999-9999\",\n      \"VAL_DE\": \"31/12/2019\",\n      \"VAL_ATE\": \"31/12/2019\",\n      \"PRODUTO\": \"Einstein Conecta|Clínicas Einstein\"\n    }\n  ]\n}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);

            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");

            //Console.WriteLine("RESPONSE DIRECT");
            //Console.WriteLine(response.Content);


            Console.WriteLine("Hello World!");
        }
    }
}
