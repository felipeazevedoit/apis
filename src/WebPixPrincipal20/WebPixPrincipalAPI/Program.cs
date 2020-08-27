using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            MainAsync().Wait();

        }
        static async Task MainAsync()
        {
            var url = await AuxNotStatic.GetInfoMotorAux("Principal", 1);
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(url.Url)
                //.UseUrls("http://localhost:5000")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
