using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Portfolio.Persistence.Context;
using System;
using Portfolio.Application.Interfaces;
using Portfolio.Application;
using Portfolio.Persistence.Interfaces;
using Portfolio.Persistence;

namespace Portfolio.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PortfolioContext>(
                options => options
                    .UseSqlite(Configuration
                        .GetConnectionString("Default.Sqlite"))
                );

            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IConhecimentoService, ConhecimentoService>();

            services.AddScoped<IGeralPersistence, GeralPersistence>();
            services.AddScoped<IProjetoPersistence, ProjetoPersistence>();
            services.AddScoped<IPerfilPersistence, PerfilPersistence>();
            services.AddScoped<IConhecimentoPersistence, ConhecimentoPersistence>();    
            

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio.API v1"));
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
