using System;

namespace HomeTask.Models
{
    public class ChatSession
    {
        public string RequestId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Status { get; set; }
        public int PollCount { get; set; }
        public DateTime LastPolledAt { get; set; }
    }
}