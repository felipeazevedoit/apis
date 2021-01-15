using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using WpComunicacoes.Entidades;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpComunic.Tradutor
{
    public class TradutorDataTable
    {
        public string ReturnObjectClassOutput(DataTable table, Metodo metodo)
        {
            string tipo = metodo.TipoSaida;
            try
            {
                switch (tipo)
                {
                    case "Array":
                        return GetArrayTable(table, metodo);
                    case "Unique":
                        return GetObjectTable(table, metodo);
                }

                return "Houve falha contate o suporte";
            }
            catch
            {
                return "Houve falha contate o suporte";
            }
            

        }

        private string GetObjectTable(DataTable table, Metodo metodo)
        {
            var classeSaida = metodo.ClasseSaida;
            var valor = "";
            List<object> ListObj = new List<object>();
            JObject jObject = new JObject();
            foreach (DataRow row in table.Rows)
            {
               
                foreach (DataColumn column in row.Table.Columns)
                {
                    string nomeColuna = "";
                    string coluna = column.ColumnName;
                    foreach (Propriedades props in classeSaida.propriedades)
                    {
                        if (props.NomeExterno == coluna)
                            nomeColuna = props.Nome;
                    }
                    if (nomeColuna == "")
                        break;
                    valor = row[coluna].ToString();

                    JProperty property = new JProperty(nomeColuna, valor);
                    jObject.Add(property);
                }
                break;
            }
            string output = JsonConvert.SerializeObject(jObject);
            return output;
        }

        private string GetArrayTable(DataTable table, Metodo metodo)
        {
            var classeSaida = metodo.ClasseSaida;
            var valor = "";
            List<object> ListObj = new List<object>();
            foreach (DataRow row in table.Rows)
            {
                JObject jObject = new JObject();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string nomeColuna = "";
                    string coluna = column.ColumnName;
                    foreach (Propriedades props in classeSaida.propriedades)
                    {
                        if (props.NomeExterno == coluna)
                            nomeColuna = props.Nome;
                    }
                    if (nomeColuna == "")
                        break;
                    valor = row[coluna].ToString();
                    
                    JProperty property = new JProperty(nomeColuna, valor);
                    jObject.Add(property);
                }
                ListObj.Add(jObject);

            }
            string output = JsonConvert.SerializeObject(ListObj);
            return output;
        }

    }
}
