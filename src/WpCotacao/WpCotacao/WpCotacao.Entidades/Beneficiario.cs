using System;
using System.Collections.Generic;
using System.Text;
using WpCotacao.Entidades.Enums;

namespace WpCotacao.Entidades
{
    public class Beneficiario 
    {
        public int Id { get; set; }
        public TipoBeneficiario tipoBeneficiario { get; set; }
        public Pet infoPet { get; set; }
        public Pessoa infoPessoa { get; set; }
        public Juridico infoJuridico { get; set; }

    }
}
