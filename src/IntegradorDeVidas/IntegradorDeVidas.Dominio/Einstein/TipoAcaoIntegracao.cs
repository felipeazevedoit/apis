using System.ComponentModel;

namespace IntegradorDeVidas.Dominio.Einstein
{
    public enum TipoAcaoIntegracao
    {
        [Description("Url Padrão")]
        UrlPadrao = 0,
        [Description("Recuperar Root Usuario / Token")]
        Login = 1,
        [Description("Cadastrar importação de Vidas")]
        CadastroVidas = 2,
    }
}