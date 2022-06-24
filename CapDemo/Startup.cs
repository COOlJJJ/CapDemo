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
                // ʧ��������ߴΣ�Ĭ����50��
                a.FailedRetryCount = 15;
                // ʧ�����Լ����Ĭ����60s
                a.FailedRetryInterval = 30;
                // ����������Դ����Ļص�
                a.FailedThresholdCallback = failedInfo =>
                {
                    // ����ͨ�����ʼ������ŵ���Ϣ֪ͨ�˹�����ϵͳ�����Զ��㶨��
                    Console.WriteLine("��Ҫ�˹�������");
                };
                // ���÷�����Ϣ���߳����ݣ�����1֮�󣬲���֤��Ϣ˳��
                //a.ProducerThreadCount = 1;
                // �ɹ���Ϣ�����ʱ�䣬����Ϊ��λ��Ĭ����1��
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
