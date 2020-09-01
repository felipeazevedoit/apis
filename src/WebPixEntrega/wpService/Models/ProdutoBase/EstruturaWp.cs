namespace wpService.Models.ProdutoBase
{
    public class EstruturaWp : BaseModel
    {
        public int Tipo { get; set; }
        public int idPermissao { get; set; }
        public int idPai { get; set; }
        public string Principal { get; set; }
        public string Imagem { get; set; }
        public string ImagemMenu { get; set; }
        public string LinkManual { get; set; }
        public string UrlManual { get; set; }
        public int Ordem { get; set; }
    }
}
