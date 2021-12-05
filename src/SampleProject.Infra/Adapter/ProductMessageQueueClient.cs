using System;
using System.Threading.Tasks;
using SampleProject.Domain.Interface;

namespace SampleProject.Infra.Adapter
{
    public class ProductMessageQueueClient : IMessageQueueClient
    {
        public Task BuildMessage<T>(T input)
        {
            throw new NotImplementedException();
        }

        public Task CheckHealth()
        {
            throw new NotImplementedException();
        }

        public Task SendMessage<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}