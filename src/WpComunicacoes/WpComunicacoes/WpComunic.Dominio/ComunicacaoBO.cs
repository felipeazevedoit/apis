using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpComunic.Repositorio;
using WpComunic.Servico;
using WpComunicacoes.Entidades;
using WpComunic.database.oracle;

namespace WpComunic.Dominio
{
    public class ComunicacaoBO
    {

        public string RealizaComunicacao(MotorExterno motor)
        {

            switch (motor.tipo)
            {
                case "rest":
                    return comunicRest(motor.metodo.FirstOrDefault());
                case "database":
                    return comunicDatabase(motor);
                case "soa":
                    return comunicSOA(motor.metodo.FirstOrDefault());
                case "wpf":
                    return  comunicWpf(motor.metodo.FirstOrDefault());
            }
            return null;
        }

        private string comunicWpf(Metodo metodo)
        {
            throw new NotImplementedException();
        }

        private string comunicSOA(Metodo metodo)
        {
            throw new NotImplementedException();
        }

        private string comunicRest(Metodo metodo)
        {
            throw new NotImplementedException();
        }

        private string comunicDatabase(MotorExterno motor)
        {
            var metodo = motor.metodo.FirstOrDefault();
            switch (metodo.Tipo)
            {
                case "oracle":
                    ComunicOracle oracle = new ComunicOracle();
                    return oracle.RealizarComunicacaoOracle(motor);

                    /////Implementar as outras comunicações

            }
            return null;
        }

        public object GetPropValue(object src, string propName)
        {
            JObject obj = JObject.Parse(src.ToString());
            object VA = obj[propName];

            return VA;
        }
    }



}
