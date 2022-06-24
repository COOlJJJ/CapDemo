using CapStockRespository;
using CapStockRespository.Respository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStockDemo
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
            services.AddControllers();
            services.AddDbContext<StockDbContext>(db => db.UseSqlServer(@"Server=localhost;User Id=Demo;Password=12345678;Database=CapStockDemo;MultipleActiveResultSets=true",
                b => b.MigrationsAssembly("CapStockDemo")));

            services.AddTransient<ICapStockRespository, CapStockRespository.Respository.CapStockRespository>();

            services.AddCap(a =>
            {
                a.UseEntityFramework<StockDbContext>();
                a.UseSqlServer(@"Server=localhost;User Id=Demo;Password=12345678;Database=CapStockDemo;MultipleActiveResultSets=true");

                a.UseRabbitMQ(rb =>
                {
                    rb.HostName = "127.0.0.1";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                    rb.Port = 5672;
                    rb.VirtualHost = "/";
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
