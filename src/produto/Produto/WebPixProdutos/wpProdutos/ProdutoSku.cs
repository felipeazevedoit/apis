using System.Collections.Generic;

namespace Entity
{
    public class ProdutoSku : Base
    {
        public int IDProduto { get; set; }
        public string CodSkuExterno { get; set; }
        public int SkuEstoque { get; set; }
        public int SkuPeso { get; set; }
        public List<Propiedades> Propiedade { get; set; } 

    }
}
