using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WpFinanceiro.Domains;
using WpFinanceiro.Helpers;
using WpFinanceiro.Infrastructure;
using WpFinanceiro.Services;

namespace WpFinanceiro
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
            services.AddTransient<ExtratoRepository>();
            services.AddTransient<NaturezaRepository>(); 
            services.AddTransient<DadosBancariosDomain>();
            services.AddTransient<DadosBancariosRepository>();
            services.AddTransient<ExtratoDomain>(); 
            services.AddTransient<EmailHandler>();
            services.AddTransient<ConfiguracaoService>();
            services.AddTransient<EmailService>();
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
