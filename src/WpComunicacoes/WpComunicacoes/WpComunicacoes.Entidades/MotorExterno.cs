using System.Collections.Generic;

namespace WpComunicacoes.Entidades
{
    public class MotorExterno : Base
    {
        public Funcao funcao
        {
            get;
            set;
        }
        public string tipo
        {
            get;
            set;
        }
        public List<Metodo> metodo
        {
            get;
            set;
        }
        public string saida
        {
            get;
            set;

        }

    }
}