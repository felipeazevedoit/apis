using System;
using System.Collections.Generic;
using System.Text;

namespace WpComunicacoes.Entidades
{
    public class DataBase : Base
    {
        public string connectionString
        {
            get; set;
        }
        public MotorExterno motor
        {
            get; set;
        }
        public string tipo
        {
            get; set;
        }
    }
}
