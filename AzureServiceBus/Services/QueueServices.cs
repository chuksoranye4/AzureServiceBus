using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;
using System.Text;

namespace AzureServiceBus.Services
{
    public class QueueServices : IQueueServices
    {
        public IConfiguration _config { get; }
        public QueueServices(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsyn<T>(T message, string queuename)
        {
            var queClient = new QueueClient(_config.GetConnectionString("AzureBusServiceCon"), queuename);
            string msgBody = JsonSerializer.Serialize(message);
            var msg = new Message(Encoding.UTF8.GetBytes(msgBody));

            await queClient.SendAsync(msg);
        }
    }
}
