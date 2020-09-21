namespace WpPagamentos.Entidade
{
    public class Propriedades : Base
    {
        public int Moeda { get; set; }
        public bool Recalculo { get; set; }
        public bool Meio { get; set; }
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public string tidErede { get; set; }      

    }
}