using IntegradorDeVidas.Dominio.Einstein;
using IntegradorDeVidas.Integracoes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegradorDeVidas.Servico
{

    public class Einstein
    {
        public Einstein()
        {

        }

        public bool CadastrarVidas(Vida vida)
        {
            var import = new ImportadorVidas(vida);

            new EinsteinIntegracao().CadastrarVidas(import);
            return true;

        }


    }
}
