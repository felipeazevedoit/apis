using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using WpComunic.Repositorio;
using WpComunic.Tradutor;
using WpComunicacoes.Entidades;

namespace WpComunic.database.oracle
{
    public class ComunicOracle
    {
        public string RealizarComunicacaoOracle(MotorExterno motor)
        {
            var metodo = motor.metodo.First();
            DataBaseRep dataBaseRep = new DataBaseRep();
            DataBase database = dataBaseRep.GetDataBaseByTipo("Oracle");
            TradutorDataTable saida = new TradutorDataTable();
            switch (metodo.Meio)
            {
                case "proc":
                    return saida.ReturnObjectClassOutput(comunicOracleProc(metodo, database),metodo);
                case "query":
                    return comunicOracleQuery(metodo, database);
            
            }
            return null;


        }

        private string comunicOracleQuery(Metodo metodo, DataBase database)
        {
            throw new NotImplementedException();
        }

        private DataTable comunicOracleProc(Metodo metodo, DataBase database)
        {
            var cnn = database.connectionString;
            using OracleConnection con = new OracleConnection(cnn);
            //using OracleCommand cmd = con.CreateCommand();
            try
            {
                con.Open();
                //cmd.BindByName = true;
                DataTable dt = new DataTable();

                OracleCommand cmd = new OracleCommand(metodo.Endpoint, con);


                foreach (Propriedades prop in metodo.ClasseEntrada.propriedades)
                {
                    if (prop.tipo == "string")
                        cmd.Parameters.Add(new OracleParameter(prop.NomeExterno, OracleDbType.Varchar2, prop.valor, ParameterDirection.Input));
                    if (prop.tipo == "int")
                        cmd.Parameters.Add(new OracleParameter(prop.NomeExterno, OracleDbType.Int32, Convert.ToInt32(prop.valor), ParameterDirection.Input));
                    if (prop.tipo == "cursor")
                        cmd.Parameters.Add(new OracleParameter("outCursor", OracleDbType.RefCursor, ParameterDirection.Output));
                    if (prop.tipo == "bool")
                        cmd.Parameters.Add(new OracleParameter(prop.NomeExterno, OracleDbType.Int32, Convert.ToBoolean(prop.valor), ParameterDirection.Input));


                }
                cmd.CommandType = CommandType.StoredProcedure;
                OracleDataAdapter sd = new OracleDataAdapter(cmd);
                // con.Open();

                sd.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
               
                return null;

            }
        }
    }
}
