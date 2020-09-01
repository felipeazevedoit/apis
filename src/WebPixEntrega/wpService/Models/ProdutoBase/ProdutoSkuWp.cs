using System.Collections.Generic;

namespace wpService.Models.ProdutoBase
{
    public class ProdutoSkuWp : BaseModel
    {
        public int IDProduto { get; set; }
        public string CodSkuExterno { get; set; }
        public int SkuEstoque { get; set; }
        public int SkuPeso { get; set; }
        public List<PropiedadesWp> Propiedade { get; set; }
    }
}
