using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace WpMidias
{
    public static class SwaggerConfig
    {
        private static string sTitle = "Api Midia";

        public static void Register(IServiceCollection _services)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = sTitle,
                        Version = "v1",
                        Description = ""
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;

                var archivesComments = Directory.GetFiles(caminhoAplicacao)
                            .Where(x => new FileInfo(x).Name.Contains(nomeAplicacao.Replace("Api", "")) && new FileInfo(x).Extension == ".xml").ToList();
                archivesComments.ForEach(x =>
                {
                    c.IncludeXmlComments(x);
                });

            });
        }

        public static void RegisterUI(IApplicationBuilder app, IConfiguration configuration)
        {
            string pathVirtual = configuration["VirtualDirectory"];

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{pathVirtual}/swagger/v1/swagger.json",
                    sTitle);

                c.DocumentTitle = sTitle;

            });
        }
    }
}
