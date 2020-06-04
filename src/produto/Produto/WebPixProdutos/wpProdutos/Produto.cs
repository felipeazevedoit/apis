using System.Collections.Generic;

namespace Entity
{
    public class Produto : Base
    {
        public string CodExterno { get; set; }
        public string Fabrinte { get; set; }
        public string Estoque { get; set; }
        public List<ProdutoSku> Sku { get; set; }
        public int Peso { get; set; }
        public int Catalogo { get; set; }
        public int Comprimento { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int EstoqueMinimo { get; set; }
        public string DescricaoLonga { get; set; }
        public Preco Preco { get; set; }
        
    }
}
