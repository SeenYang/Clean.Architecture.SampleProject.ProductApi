using System;
using System.Text.Json;
using System.Threading.Tasks;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;

namespace SampleProject.Infra.Adapter
{
    public class ProductMessageQueueClient : IMessageQueueClient
    {
        /// <summary>
        /// [WIP]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="msgType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public QueueMessageTemplate BuildMessage<T>(T input, MessageType msgType)
        {
            return new QueueMessageTemplate
            {
                Date = DateTime.UtcNow,
                MessageType = msgType.ToString(),
                Message = JsonSerializer.Serialize(input)
            };
        }

        public async Task<bool> CheckHealth()
        {
            // TODO
            return true;
        }

        /// <summary>
        /// [WIP]
        /// Send message into queue and return true/false
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SendMessage<T>(MessageType messageType, T message)
        {
            // TODO
            return true;
        }
    }
}