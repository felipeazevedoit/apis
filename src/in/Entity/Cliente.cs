using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Cliente : Base
    {
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }
}
