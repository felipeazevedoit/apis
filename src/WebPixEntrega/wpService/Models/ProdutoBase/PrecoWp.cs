using System;

namespace wpService.Models.ProdutoBase
{
    public class PrecoWp : BaseModel
    {
        public int IDProduto { get; set; }
        public int PrecoReal { get; set; }
        public int PrecoPromocional { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
