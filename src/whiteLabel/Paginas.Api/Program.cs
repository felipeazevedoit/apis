﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Paginas.Api.Helpers;

namespace Paginas.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            MainAsync(args).Wait();

        }
        static async Task MainAsync(string[] args)
        {
            var url = await AuxNotStatic.GetInfoMotorAux("Paginas", 1);
            var host = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(url.Url)
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
