using Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TaxaJurosApiConfig>(Configuration.GetSection(nameof(TaxaJurosApiConfig)));

            //Cria��o do Singleton
            services.AddSingleton<ITaxaJurosApiConfig>(sp =>
                sp.GetRequiredService<IOptions<TaxaJurosApiConfig>>().Value);

            //Pol�tica de Retry - Resili�ncia com Polly
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            //Registrar HttpClient
            services.AddHttpClient<ITaxaJurosService, TaxaJurosService>(
                x => x.BaseAddress = new Uri(Configuration["TaxaJurosApiConfig:BaseUrl"]))
                .AddPolicyHandler(retryPolicy);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API para C�lculo da taxa de Juros",
                    Description = "Teste Softplan",
                    Contact = new OpenApiContact
                    {
                        Name = "Fabio Navarro",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/fabio-piola-navarro/"),
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/Ecommerce-{Date}.txt");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - c�lculo de Juros");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
