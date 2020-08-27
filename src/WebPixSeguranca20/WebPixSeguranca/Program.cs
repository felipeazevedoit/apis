using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WebPixSeguranca.Helper.Auxiliares;

namespace WebPixSeguranca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            MainAsync().Wait();

        }
        static async Task MainAsync()
        {
            var url = await AuxNotStatic.GetInfoMotorAux("Seguranca", 1);
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(url.Url)
                //Lucas
               // .UseUrls("http://localhost.com:5300")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }

}
