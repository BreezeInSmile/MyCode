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

namespace MyCode
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC注册
            services.AddControllersWithViews();

            /*
             * AddTransient 暂时生存期服务，是每次从服务容器进行请求时创建的。 这种生存期适合轻量级、 无状态的服务
             * AddScoped 作用域生存期服务，以每个客户端请求（连接）一次的方式创建
             * AddSingleton 单一实例生存期服务，是在第一次请求时（或者在运行 Startup.ConfigureServices 并且使用服务注册指定实例时）创建的。 每个后续请求都使用相同的实例。如果应用需要单一实例行为，建议允许服务容器管理服务的生存期。不要实现单一实例设计模式并提供用户代码来管理对象在类中的生存期
             */
            services.AddSingleton<IClock, ChinaClock>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();

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

            app.UseStaticFiles();//使用静态文件

            app.UseHttpsRedirection();//把http请求转换为https请求

            app.UseAuthentication();//身份验证

            app.UseRouting();//路由中间件

            app.UseEndpoints(endpoints => //端点中间件
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
