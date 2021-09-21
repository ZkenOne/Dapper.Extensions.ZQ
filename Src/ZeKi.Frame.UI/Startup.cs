using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.Extensions.Autofac;
using AspectCore.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling;
using ZeKi.Frame.Common;
using ZeKi.Frame.IBLL;
using ZeKi.Frame.UI.Filters;
using ZeKi.Frame.UI.Handler;
using ZeKi.Frame.UI.Middleware;

namespace ZeKi.Frame.UI
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
            services.AddMemoryCacheSetup();
            //services.AddRedisCacheSetup();

            //����������滻����
            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddControllers((mvcOption) =>
            {
                //AddService��Ҫ��DI����ע��(Add����Ҫ,���Բ���ʹ��DI)
                mvcOption.Filters.AddService<GlobalErrorFilterAttribute>();
                mvcOption.Filters.AddService<ProfilingFilterAttribute>();
                //mvcOption.Filters.AddService<ExampleFilterAttribute>();
            });

            //ע��CurrencyClient �� ���һ����վ�����м��
            services.AddHttpClient<ICurrencyClient, CurrencyClient>()
                .AddHttpMessageHandler<GlobalHttpHandler>();

            ////ע�����ö�̬����(AspectCore)[AutoFac��AspectCore����ʱ ��ConfigureContainer����]
            //services.ConfigureDynamicProxy();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region ����AspectCore
            //var serviceContext = new ServiceContext();
            //builder.Populate(serviceContext);
            //var configuration = serviceContext.Configuration;
            ////����RegisterDynamicProxy��չ������Autofac��ע�ᶯ̬�������Ͷ�̬��������
            //builder.RegisterDynamicProxy(configuration, config =>
            //{
            //    //ȫ��ע�����Ҫ�ڷ�����������
            //    //config.Interceptors.AddTyped<MethodExecuteLoggerInterceptor>(Predicates.ForService("*Service"));
            //});

            //����RegisterDynamicProxy��չ������Autofac��ע�ᶯ̬�������
            //������ȫ����Ҫ �ֶ��ڷ����ϴ�����
            builder.RegisterDynamicProxy();
            #endregion

            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new AutofacModuleRegister());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //, IHost host
            //��ʱ��ϵͳ��ʼ����ɣ��ӿں���Ӧ��ע����Ϻ����ȡĳ���ӿڽ����Զ����ʼ����
            //�����ʼ���Զ��幤���������ⲿDLL,�ڲ�֪�ⲿ������½��г�ʼ�������Լ��ķ���
            //using (var container = host.Services.CreateScope())
            //{
            //    var sysUserInfoBLL = container.ServiceProvider.GetService<ISysUserInfoBLL>();
            //}

            //ע���Զ����м��
            app.UseIPIntercept();

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
