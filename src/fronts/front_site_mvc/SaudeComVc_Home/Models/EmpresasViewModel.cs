using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class EmpresasViewModel : BaseModel
    {
        public EmpresasViewModel(string cNPJ)
        {
            CNPJ = cNPJ;
        }

        public EmpresasViewModel()
        {


        }

        public string CNPJ { get; set; }

    }
}