using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Example.Common.Services;

namespace Reactive.Example.Common.Extensions
{
    public static class RabbitBuilderExtensions
    {
        private static IRabbitListener _listener { get; set; }

        public static IApplicationBuilder UseRabbitListener<T>(this IApplicationBuilder app) where T: IRabbitListener
        {
//            _listener = app.ApplicationServices.GetService<IRabbitListener>();
            _listener = app.ApplicationServices.GetService<T>();

            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);

            //press Ctrl+C to reproduce if your app runs in Kestrel as a console app
            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _listener.Register();
        }

        private static void OnStopping()
        {
            _listener.Deregister();    
        }
    }
}