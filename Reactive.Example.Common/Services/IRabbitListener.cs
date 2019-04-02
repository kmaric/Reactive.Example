namespace Reactive.Example.Common.Services
{
    public interface IRabbitListener
    {
        void Register();
        void Deregister();
    }
}