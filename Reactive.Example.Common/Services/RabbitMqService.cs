using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Reactive.Example.Common.Config;
using Reactive.Example.Common.Interfaces;

namespace Reactive.Example.Common.Services
{
    public class RabbitMqService: IRabbitMqService
    {
        private readonly RabbitMqModel _settings;
        private Task Initialization;
        private IConnection _rabbitConnection;
        
        public RabbitMqService(IOptionsSnapshot<RabbitMqModel> _settings)
        {
            this._settings = _settings.Value;
            Initialization = Start();
        }

        private async Task Start()
        {
            Console.WriteLine("Starting rabbit-mq initialization");
            await Connect();
        }

        private async Task<bool> Connect()
        {
            if (_rabbitConnection?.IsOpen == true)
            {
                return true;
            }
            
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                VirtualHost = _settings.VHost,
                UserName = _settings.Username,
                Password = _settings.Password,
                RequestedHeartbeat = 60,
                AutomaticRecoveryEnabled = true,
                Port = _settings.Port
            };

            try
            {
                _rabbitConnection = factory.CreateConnection();
                using (var channel = _rabbitConnection.CreateModel())
                {
                    channel.ExchangeDeclare(_settings.Exchange, "topic", true, false, null);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"RabbitMq Connection Failed {e.Message}");
            }

            return _rabbitConnection != null ? _rabbitConnection.IsOpen : false;

        }

        public IModel CreateListener(string queue, bool durable = false, ushort prefetch = 10)
        {
            var channel = _rabbitConnection.CreateModel();
            channel.QueueDeclare(
                queue: queue,
                durable: durable,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            
            channel.BasicQos(0, prefetch, true);

            return channel;
        }
    }
}