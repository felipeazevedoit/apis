using IntegradorDeVidas.Dominio.Einstein;
using System;

namespace IntegradorDeVidas.Teste.HardCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var vida = new Vida();
            vida.EMAIL = "teste@teste.com.br";
            vida.FONE = "11 985896525";
            vida.DNAS = "teste";
            vida.NCPF = "58778545899";
            vida.NOME = "José Teste";
            vida.PRODUTO = "0";
            vida.

            var root = new Servico.Einstein();
            Console.Write(root.user.email);
            Console.WriteLine(root.token);


            Console.WriteLine("Hello World!");
        }
    }
}
