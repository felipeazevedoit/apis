namespace Entity
{
    public class Produto : Base
    {
        public string CodExterno { get; set; }
        public string Fabrinte { get; set; }
        public int Estoque { get; set; }
        public int EstoqueMinimo { get; set; }
        public double Preco { get; set; }
    }
}