using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixSeguranca.Helper.Auxiliares
{
    public class ConfigurationHelper
    {
        public IConfigurationRoot Configuration { get; }
        public ConfigurationHelper()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public string GetConfiguration(string name)
        {
            string appSettings = Configuration[name];
            return appSettings;
        }

    }
}
