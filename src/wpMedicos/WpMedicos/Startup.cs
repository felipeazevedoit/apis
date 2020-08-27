using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WpMedicos.Domains;
using WpMedicos.Infrastructure;
using WpNoticias.Services;

namespace WpMedicos
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
            services.AddTransient<MedicoDomain>();
            services.AddTransient<EspecialidadeDomain>();
            services.AddTransient<MedicoRepository>();
            services.AddTransient<EspecialidadeRepository>();
            services.AddTransient<MedicosXEspecialidadesDomain>();
            services.AddTransient<MedicosXEspecialidadesRepository>();
            services.AddTransient<MedicoXPacienteDomain>();
            services.AddTransient<MedicoXPacienteRepository>();
            services.AddTransient<EnderecoDomain>();
            services.AddTransient<EnderecoRepository>();
            services.AddTransient<TelefoneDomain>();
            services.AddTransient<TelefoneRepository>();
            services.AddTransient<MedicoXClinicasRepository>();
            services.AddTransient<MedicoXClinicasDomain>();

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
