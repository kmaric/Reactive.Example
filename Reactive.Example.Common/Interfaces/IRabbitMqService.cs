using RabbitMQ.Client;

namespace Reactive.Example.Common.Interfaces
{
    public interface IRabbitMqService
    {
        IModel CreateListener(string queue, bool durable = false);
    }
}