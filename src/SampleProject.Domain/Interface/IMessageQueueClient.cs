using System.Threading.Tasks;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interface
{
    public interface IMessageQueueClient
    {
        QueueMessageTemplate BuildMessage<T>(T input, MessageType msgType);
        Task<bool> CheckHealth();
        Task<bool> SendMessage<T>(MessageType messageType, T message);
    }
}