using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureServiceBus.Services
{
    public interface IQueueServices
    {
        IConfiguration _config { get; }

        Task SendMessageAsyn<T>(T message, string queuename);
    }
}