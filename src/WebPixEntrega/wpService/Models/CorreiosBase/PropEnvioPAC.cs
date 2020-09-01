using System;

namespace wpService.Models
{
    public class PropEnvioPAC
    {
        public string nCdEmpresa { get; set; }
        public string sDsSenha { get; set; }
        public string nCdServico { get; set; }
        public string sCepOrigem { get; set; }
        public string sCepDestino { get; set; }
        public string nVlPeso { get; set; }
        public int nCdFormato { get; set; }
        public Decimal nVlComprimento { get; set; }
        public Decimal nVlAltura { get; set; }
        public Decimal nVlLargura { get; set; }
        public Decimal nVlDiametro { get; set; }
        public string sCdMaoPropria { get; set; }
        public Decimal nVlValorDeclarado { get; set; }
        public string sCdAvisoRecebimento { get; set; }
    }
}
