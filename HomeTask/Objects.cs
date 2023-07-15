using HomeTask.Models;
using System.Collections.Generic;

namespace HomeTask
{
    public class Objects
    {
        public const int MaxQueueSize = 10;
        public const int MaxConcurrentChatsPerAgent = 10;

        public static readonly Queue<ChatSession> _chatQueue = new Queue<ChatSession>();
        public static readonly Dictionary<string, ChatSession> _activeChatSessions = new Dictionary<string, ChatSession>();

        public static readonly List<Agent> teamA = new()
        {
            new Agent { Name = "Team Lead", Seniority = AgentSeniority.TeamLead, Capacity = 8 },
            new Agent { Name = "Mid-Level", Seniority = AgentSeniority.MidLevel, Capacity = 4 },
            new Agent { Name = "Mid-Level", Seniority = AgentSeniority.MidLevel, Capacity = 4 },
            new Agent { Name = "Junior", Seniority = AgentSeniority.Junior, Capacity = 4 }
        };
        public static readonly List<Agent> teamB = new()
        {
            new Agent { Name = "Senior", Seniority = AgentSeniority.Senior, Capacity = 8 },
            new Agent { Name = "Mid-Level", Seniority = AgentSeniority.MidLevel, Capacity = 4 },
            new Agent { Name = "Junior", Seniority = AgentSeniority.Junior, Capacity = 4 },
            new Agent { Name = "Junior", Seniority = AgentSeniority.Junior, Capacity = 4 }
        };
        public static readonly List<Agent> teamC = new()
        {
            new Agent { Name = "Mid-Level", Seniority = AgentSeniority.MidLevel, Capacity = 8 },
            new Agent { Name = "Mid-Level", Seniority = AgentSeniority.MidLevel, Capacity = 8 }
        };
        public static readonly List<Agent> overflowTeam = new List<Agent>();

        public static int teamAIndex = 0;
        public static int teamBIndex = 0;
        public static int teamCIndex = 0;
        public static int overflowIndex = 0;

        public static readonly Dictionary<AgentSeniority, double> seniorityMultipliers = new Dictionary<AgentSeniority, double>
        {
            { AgentSeniority.Junior, 0.4 },
            { AgentSeniority.MidLevel, 0.6 },
            { AgentSeniority.Senior, 0.8 },
            { AgentSeniority.TeamLead, 0.5 }
        };
    }
}