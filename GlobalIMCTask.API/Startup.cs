using GlobalIMCTask.Core.Contexts;
using GlobalIMCTask.Domain.Products;
using GlobalIMCTask.Repositories.Products;
using GlobalIMCTask.Services.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API
{
    public class Startup
    {
        readonly string AllowUIPolicy = "_AllowUIPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dbName = Configuration["DatabaseName"];
            services.AddScoped(options =>
            {
                return new TaskContext(Directory.GetCurrentDirectory() + "\\" + dbName);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ProductsLogic>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlobalIMCTask.API", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowUIPolicy,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlobalIMCTask.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(AllowUIPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
