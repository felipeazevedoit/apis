using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpComunic.Infra.Infraestrutura;
using WpComunicacoes.Entidades;

namespace WpComunic.Repositorio
{
    public class DataBaseRep : Base<DataBase>
    {
        public DataBase GetDataBaseByTipo(String tipo)
        {

            using (var context = new WpContext())
            {
                var database = context.database.Include(database => database.motor)
                    .Where(x => x.tipo == tipo).First();
                return database;
            }

        }
    }
}
