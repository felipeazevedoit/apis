namespace wpService.Models.ProdutoBase
{
    public class ArquivoWp : BaseModel
    {
        public string Tipo { get; set; }
        public string CaminhoLogico { get; set; }
        public string CaminhoVirtual { get; set; }
        public byte[] BArquivo { get; set; }
    }
}
