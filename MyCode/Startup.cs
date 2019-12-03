using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCode.Services;
using Microsoft.Extensions.Configuration;
using MyCode.Models;

namespace MyCode
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration){
            _configuration=configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //MVCע��
            services.AddControllersWithViews();

            /*
             * AddTransient ��ʱ�����ڷ�����ÿ�δӷ���������������ʱ�����ġ� �����������ʺ��������� ��״̬�ķ���
             * AddScoped �����������ڷ�����ÿ���ͻ����������ӣ�һ�εķ�ʽ����
             * AddSingleton ��һʵ�������ڷ������ڵ�һ������ʱ������������ Startup.ConfigureServices ����ʹ�÷���ע��ָ��ʵ��ʱ�������ġ� ÿ����������ʹ����ͬ��ʵ�������Ӧ����Ҫ��һʵ����Ϊ��������������������������������ڡ���Ҫʵ�ֵ�һʵ�����ģʽ���ṩ�û��������������������е�������
             */
            services.AddSingleton<IClock, ChinaClock>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.Configure<MyCodeOptions>(_configuration.GetSection("MyCode"));
            //API
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();//ʹ�þ�̬�ļ�

            app.UseHttpsRedirection();//��http����ת��Ϊhttps����

            app.UseAuthentication();//������֤

            app.UseRouting();//·���м��

            app.UseEndpoints(endpoints => //�˵��м��
            {
                endpoints.MapControllerRoute("default", "{controller=Department}/{action=Index}/{id?}");
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
