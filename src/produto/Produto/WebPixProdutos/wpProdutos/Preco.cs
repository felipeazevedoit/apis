﻿using System;

namespace Entity
{
    public class Preco : Base
    {
        public int IDProduto { get; set; }
        public int PrecoReal { get; set; }
        public int PrecoPromocional { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
