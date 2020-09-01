using System;

namespace WebPixPrincipalAPI.Model
{
    public class PageViewModel : BaseViewModel
    {
        public string Titulo { get; set; }
        public Byte[] Conteudo { get; set; }
        public string Url { get; set; }
        public int idMenu { get; set; }
    }
}
