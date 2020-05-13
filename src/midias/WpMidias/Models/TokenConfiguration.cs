using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpMidias.Models
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public DateTime DtInclusao { get; set; }
    }
}
