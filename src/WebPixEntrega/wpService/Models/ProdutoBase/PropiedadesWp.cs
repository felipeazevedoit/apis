using System.Collections.Generic;

namespace wpService.Models.ProdutoBase
{
    public class PropiedadesWp : BaseModel
    {
        public List<ArquivoWp> ArquivosVinculado { get; set; }
        public List<EstruturaWp> Departamento { get; set; }
    }
}
