using System.Threading.Tasks;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interface
{
    public interface IMessageQueueClient
    {
        Task BuildMessage<T>(T input);
        Task CheckHealth();
        Task SendMessage<T>(MessageType messageType, T message);
    }
}