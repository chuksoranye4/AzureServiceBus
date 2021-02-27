using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using SharedCode;

namespace ServiceBusReader
{
    class Program
    {
        const string connString = "Endpoint=sb://personqueue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5NNs/LLECNCLovNAwRXgtnEL36G/9Qg4sYwqveOAzNo=";
        const string queueName = "personqueue";
        static IQueueClient queueClient;
        static async Task Main(string[] args)
        {
            queueClient = new QueueClient(connString, queueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessageAsyn, messageHandlerOptions);
            Console.ReadLine();
            await queueClient.CloseAsync();
        }

        private static async Task ProcessMessageAsyn(Message message, CancellationToken arg2)
        {
            var jsonstring = Encoding.UTF8.GetString(message.Body);
            PersonData personData = JsonSerializer.Deserialize<PersonData>(jsonstring);
            // now do what ever you want with the data like DB CRUD
            Console.WriteLine("Data Received : {0} {1}", personData.FirstName, personData.LastName);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine("exception message {0}", arg.Exception);
            return Task.CompletedTask;
        }
    }
}
