using System.Collections.Generic;

namespace wpService.Models.ProdutoBase
{
    public class ProdutoWp : BaseModel
    {

        public string CodExterno { get; set; }
        public string Fabrinte { get; set; }
        public string Estoque { get; set; }
        public List<ProdutoSkuWp> Sku { get; set; }
        public int Peso { get; set; }
        public int Catalogo { get; set; }
        public int Comprimento { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int EstoqueMinimo { get; set; }
        public string DescricaoLonga { get; set; }
        public PrecoWp Preco { get; set; }
    }
}
