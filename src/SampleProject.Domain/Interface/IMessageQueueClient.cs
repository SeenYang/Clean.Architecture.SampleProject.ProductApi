using System.Threading.Tasks;

namespace SampleProject.Domain.Interface
{
    public interface IMessageQueueClient
    {
        Task BuildMessage<T>(T input);
        Task CheckHealth();
        Task SendMessage<T>(T message);
    }
}