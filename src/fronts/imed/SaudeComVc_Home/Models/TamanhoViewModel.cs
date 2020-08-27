using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class TamanhoViewModel
    {
        public TamanhoViewModel(int iD, string tamanho)
        {
            ID = iD;
            Tamanho = tamanho;
        }

        public TamanhoViewModel()
        {

        }

        public int ID { get; set; }
        public string Tamanho { get; set; }
    }
}