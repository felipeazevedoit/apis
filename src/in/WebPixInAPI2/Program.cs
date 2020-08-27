using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WebPixInAPI2.Helper;

namespace WebPixInAPI2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigurationHelper configuration = new ConfigurationHelper();
            string url = configuration.GetConfiguration("URLUsage");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(url)       
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
