using Order.Broker.Interfaces;
using Order.Broker.Models;
using System.Collections;

namespace Order.Broker.Brokers
{
    public class InMemoryBroker : IBroker
    {
        public Queue _queue { get; set; }

        public InMemoryBroker() {
            _queue = new();
        }

        public Task<bool> Publish<T>(T message, EventProperties eventProperties)
        {
            var cloudEvent = CloudMessageModel<T>.Create(message, eventProperties);

            _queue.Enqueue(cloudEvent);

            return Task.FromResult(true);
        }        
    }
}
