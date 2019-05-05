using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Example.Common.Config;
using Reactive.Example.Common.Extensions;
using Reactive.Example.Common.Interfaces;
using Reactive.Example.Common.Interfaces.DAL;
using Reactive.Example.Common.Services;
using Reactive.Example.DAL.Repositories;
using Reactive.Example.Processor.Listeners;
using Reactive.Example.Processor.Services;

namespace Reactive.Example.Processor
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;   
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services.AddHostedService<Application>();
            services.AddOptions();
            
            services.Configure<RabbitMqModel>(_configuration.GetSection("RabbitMQ"));

            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddSingleton<HttpTestListener>();
            services.AddSingleton<ITestRepository, TestRepositoryDapper>();
            services.AddHostedService<TaskRunnerService>();
            
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
//            services.AddSignalR();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);
            applicationLifetime.ApplicationStarted.Register(StartClient);

            app.UseCors("CorsPolicy");
            app.UseRabbitConnection();
            app.UseRabbitListener<HttpTestListener>();
            
//            app.UseSignalR(route =>
//            {
//                route.MapHub<DeviceHub>("/hubs/device");
//            });
        }

        private void StartClient()
        {
            Console.WriteLine("Started...");
        }

        private void OnShutdown()
        {
            Console.WriteLine("Closing...");
        }
    }
}