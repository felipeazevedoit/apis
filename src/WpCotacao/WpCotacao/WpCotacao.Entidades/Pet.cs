using System;
using WpCotacao.Entidades.Enums;

namespace WpCotacao.Entidades
{
    public class Pet : Base
    {
        
        public int tipo { get; set; }
        public string CorPelo { get; set; }
        public DateTime DataNascimento { get; set; }
        public TamanhoPet tamanho { get; set; }
        public SexoPet sexo { get; set; }
        public string raca { get; set; }

        public bool Vacina { get; set; }
        public bool DoencaLesao { get; set; }

    }
}