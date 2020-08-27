using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WpNoticias.Domains;
using WpNoticias.Infrastructure;
using WpNoticias.Services;

namespace WpNoticias
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SegurancaService>();
            services.AddTransient<NoticiasDomain>();
            services.AddTransient<NoticiaRepository>();
            services.AddTransient<ComentariosDomain>();
            services.AddTransient<ComentarioRepository>();
            services.AddTransient<CategoriasDomain>();
            services.AddTransient<CategoriasRepository>();
            services.AddTransient<NoticiaXPacienteDomain>();
            services.AddTransient<NoticiaXPacienteRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
