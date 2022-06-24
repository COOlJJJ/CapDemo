using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderRespository;
using OrderRespository.Respository;
using OrderService;
using System;

namespace CapDemo
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
            services.AddDbContext<OrderDbContext>(db => db.UseSqlServer(
                @"Server=localhost;User Id=Demo;Password=12345678;Database=CapOrderDemo;MultipleActiveResultSets=true",
                b => b.MigrationsAssembly("CapOderDemo")));

            services.AddTransient<ICapOrderRespository, CapOrderRespository>();
            services.AddTransient<ICapOrderService, CapOrderService>();

            services.AddCap(a =>
            {
                // 失败重试最高次，默认是50次
                a.FailedRetryCount = 15;
                // 失败重试间隔，默认是60s
                a.FailedRetryInterval = 30;
                // 超出最高重试次数的回调
                a.FailedThresholdCallback = failedInfo =>
                {
                    // 这里通过发邮件、短信等信息通知人工处理，系统不能自动搞定了
                    Console.WriteLine("需要人工处理啦");
                };
                // 设置发送消息的线程数据，大于1之后，不保证消息顺序
                //a.ProducerThreadCount = 1;
                // 成功消息保存的时间，以秒为单位，默认是1天
                //a.SucceedMessageExpiredAfter = 12 * 3600;

                a.UseEntityFramework<OrderDbContext>();
                a.UseSqlServer(@"Server=localhost;User Id=Demo;Password=12345678;Database=CapOrderDemo;MultipleActiveResultSets=true");
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
