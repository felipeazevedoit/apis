namespace WpComunicacoes.Entidades
{
    public class Metodo : Base
    {
        public string Tipo
        {
            get;
            set;
        }
        public string Meio
        {
            get; set;
        }
        public string Endpoint
        {
            get;set;
        }
        public Classe ClasseEntrada
        {
            get; set;
        }
        public Classe ClasseSaida
        {
            get; set;
        }
    }
}