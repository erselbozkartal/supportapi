using System;

namespace HomeTask
{
    public class ChatSession
    {
        public Guid SessionId { get; set; }
        public Agent AssignedAgent { get; set; }
        public int PollCount { get; set; }
        public bool IsActive { get; set; }

        public ChatSession()
        {
            SessionId = Guid.NewGuid();
            PollCount = 0;
            IsActive = true;
        }
    }
}