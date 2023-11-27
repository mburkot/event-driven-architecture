using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Order.Broker.Interfaces;
using Order.Broker.Models;

namespace Order.Broker.Brokers
{
    public class ServiceBusBroker : IBroker, IDisposable
    {
        public readonly Lazy<ServiceBusClient> _lazyServiceBusClient;
        public readonly ILogger<ServiceBusBroker> _logger;

        public const string TOPIC_NAME = "orders-events";

        public ServiceBusBroker(string connectionString, ILogger<ServiceBusBroker> logger) 
        {
            _lazyServiceBusClient = new(new ServiceBusClient(connectionString));
            _logger = logger;
        }

        public async Task<bool> Publish<T>(T message, EventProperties eventProperties)
        {
            var cloudEvent = CloudMessageModel<T>.Create(message, eventProperties);
            ServiceBusSender sender = null;

            try
            {
                var client = _lazyServiceBusClient.Value;
                sender = client.CreateSender(TOPIC_NAME);
                await sender.SendMessageAsync(new(cloudEvent.ToString()));
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _lazyServiceBusClient.Value.DisposeAsync();
        }
    }
}
