using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Reactive.Example.Common.Services;

namespace Reactive.Example.Processor
{
    public class Application: IHostedService
    {
        public Application(RabbitMqService rabbitMqService)
        {
            
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Application started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Application stopped");
            return Task.CompletedTask;
        }
    }
}