using Order.Broker.Models;

namespace Order.Broker.Interfaces
{
    public interface IBroker
    {
        public Task<bool> Publish<T>(T message, EventProperties eventProperties);
    }
}
