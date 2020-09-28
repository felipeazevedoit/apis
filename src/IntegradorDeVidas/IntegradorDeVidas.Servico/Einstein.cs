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

        public bool CadastrarVidas(Vida vidas)
        {
            var import = new ImportadorVidas(vidas);
            if (import.ValidarListaDeVidas().Count() == 0)
            {
                new EinsteinIntegracao().CadastrarVidas(import);
                return true;
            }
            else
            {
                return false;
            }
            
        }

       
    }
}
