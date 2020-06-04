namespace Entity
{
    public class Arquivo : Base
    {
        public string Tipo { get; set; }
        public string CaminhoLogico { get; set; }
        public string CaminhoVirtual { get; set; }
        public byte[] BArquivo { get; set; }

    }
}
