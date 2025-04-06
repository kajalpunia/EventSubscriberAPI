using System.Text;
using Azure.Messaging.ServiceBus;

namespace EventSubAPI.Services
{
    public class ServiceBusClientClass
    {
        public readonly string _connectionString;
        public readonly string _queueName;
        private readonly ServiceBusSender _sender;
        public ServiceBusClientClass(){}
        public ServiceBusClientClass(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("AzureServiceBus:ConnectionString");
            _queueName = configuration.GetValue<string>("AzureServiceBus:QueueName");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Connection string can't be null or empty.");
            }

            var serviceBusClient = new ServiceBusClient(_connectionString);
            _sender = serviceBusClient.CreateSender(_queueName);
        }

        public async Task SendMessageAsync(string messageBody)
        {
            ServiceBusMessage message = new ServiceBusMessage(messageBody);
            await _sender.SendMessageAsync(message);
        }
    }
}
