using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WpPagamentos.API.Helper;
using WpPagamentos.API.Model;

namespace WpPagamentos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            MainAsync().Wait();

        }

        static async Task MainAsync()
        {
            var url = await AuxNotStatic.GetInfoMotorAux("WpPagamentos", 1);
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url.Url)
                //.UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
