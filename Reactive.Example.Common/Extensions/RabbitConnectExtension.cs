using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Example.Common.Interfaces;

namespace Reactive.Example.Common.Extensions
{
    public static class RabbitConnectExtension
    {
        private static IRabbitMqService _rabbitMqService { get; set; }
        
        public static IApplicationBuilder UseRabbitConnection(this IApplicationBuilder app)
        {
            _rabbitMqService = app.ApplicationServices.GetRequiredService<IRabbitMqService>();
            
            
            return app;
        } 
    }
}