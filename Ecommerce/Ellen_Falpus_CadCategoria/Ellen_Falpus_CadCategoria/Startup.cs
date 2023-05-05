using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Middleware;
using Ellen_Falpus_CadCategoria.Models;
using Ellen_Falpus_CadCategoria.Repository;
using Ellen_Falpus_CadCategoria.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria
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
            services.AddDbContext<EcommerceDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ISubcategoriaRepository, SubcategoriaRepository>();
            services.AddScoped<ProdutoService, ProdutoService>();
            services.AddScoped<CategoriaService,CategoriaService>();
            services.AddScoped<SubcategoriaService, SubcategoriaService>();
            services.AddScoped<CentroService, CentroService>();
            services.AddScoped<CarrinhoDeCompraService, CarrinhoDeCompraService>();
            services.AddScoped<BuscaCEPService, BuscaCEPService>();
            services.AddScoped<ProdutoDoCarrinho>();
            services.AddTransient<CategoriaRepository>();
            services.AddTransient<SubcategoriaRepository>();
            services.AddTransient<CentroRepository>();
            services.AddTransient<CarrinhoDeCompraRepository>();
            services.AddTransient<ProdutoDoCarrinhoRepository>();
            services.AddTransient<ProdutoDoCarrinho>();
          

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ellen_Falpus_CadCategoria", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IDbConnection>((sp)=> new MySqlConnection(Configuration.GetConnectionString("CategoriaConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ellen_Falpus_CadCategoria v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware)); 

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
