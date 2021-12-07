using System;

namespace SampleProject.Domain.Models
{
    public class QueueMessageTemplate
    {
        public DateTime Date { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
    }
}