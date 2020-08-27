using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaudeComVc_Home.Helpers;
using TServices.Comum.Helpers;
using TServices.Comum.Helpers.Token;
using WpMidias.Domains;
using WpMidias.Domains.Helpers;
using WpMidias.Infrastructure;
using WpMidias.Models;
using WpMidias.Services;

namespace WpMidias
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string swaggerHabilit { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MidiasDomain>();
            services.AddTransient<SegurancaService>();
            services.AddTransient<MidiasRepository>();
            services.AddTransient<FileSystemManager>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            RegisterToken(services);

            swaggerHabilit = Configuration["HabSwagger"];
            if (bool.Parse(swaggerHabilit))
            {
                SwaggerConfig.Register(services);
            }

            #region Compression

            services.Configure<GzipCompressionProviderOptions>(
               options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (bool.Parse(swaggerHabilit))
            {
                SwaggerConfig.RegisterUI(app, Configuration);
            }

            app.UseMvc();
        }

        protected void RegisterToken(IServiceCollection services)
        {
            #region  Tokens
            var tokenConfigurations = new TokenConfiguration();
            var configToken = new List<ConfiguracaoApp>();

            
            var serviceConsuming = new ConsumingApiRest(Configuration["Seguranca-Host"],string.Empty);
            List<string> str = new List<string>();
            var ret = serviceConsuming.Execute<List<TokenConfiguration>>(
                Configuration["Seguranca-Configuracao-Api"].ToString(),
                           null,
                           RestSharp.Method.POST,
                           RestSharp.ParameterType.QueryString);
            if (ret != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.RequestResponse<List<ConfiguracaoApp>>>(ret.Content);
                configToken = result.Data;
            }               
            else
                return;

            //Assinatura do token
            string _signConfig = configToken.FirstOrDefault(x => x.Chave == "Token-SignConfig").Valor;
            var signingConfigurations = new SigningConfigurations(TServices.Comum.Helpers.Encrypt.Encrypt.GetMd5(_signConfig));

            services.AddSingleton(signingConfigurations);

            if (configToken.Count() == 0)
            {
                tokenConfigurations.Issuer = "SegurancaIssuer";
                tokenConfigurations.Audience = "SegurancaAudience";
            }
            else
            {
                tokenConfigurations.Issuer = configToken.FirstOrDefault(x => x.Chave == "Token-Issuer").Valor;
                tokenConfigurations.Audience = configToken.FirstOrDefault(x => x.Chave == "Token-Audience").Valor;
            }

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                     Configuration.GetSection("TokenConfiguration"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion
        }
    }
}
