using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class EmailModel
    {
        public EmailModel(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public EmailModel()
        {

        }

        public string Nome { get; set; }
        public string Email { get; set; }
    }
}