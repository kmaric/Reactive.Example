using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Reactive.Example.Common.Services;

namespace Reactive.Example.Processor.Listeners
{
    public class HttpTestListener: IRabbitListener
    {
        private readonly RabbitMqService _rabbitMqService;
        private IModel Listener;
        private const string Queue = "http-test-queue";
        
        public HttpTestListener(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        
        public void Register()
        {
            Listener = _rabbitMqService.CreateListener(Queue);
            
            var consumer = new EventingBasicConsumer(Listener);
            consumer.Received += Handler;

            Listener.BasicConsume(Queue, false, consumer);
        }

        public void Deregister()
        {
            Listener.Dispose();
        }
        
        private void Handler(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Body);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}