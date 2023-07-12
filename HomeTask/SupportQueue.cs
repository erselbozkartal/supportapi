using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HomeTask
{
    public class SupportQueue
    {
        private Queue<ChatSession> queue;
        private List<Agent> agents;
        private List<Agent> overflowTeam;
        private int maxQueueLength;
        private int currentQueueLength;
        private int shiftDuration;
        private int maxConcurrentChats;

        public SupportQueue()
        {
            queue = new Queue<ChatSession>();
            agents = new List<Agent>();
            overflowTeam = new List<Agent>();
            maxQueueLength = 0;
            currentQueueLength = 0;
            shiftDuration = 8; // In hours
            maxConcurrentChats = 10;
        }

        public void EnqueueChatSession(ChatSession chatSession)
        {
            if (currentQueueLength >= maxQueueLength)
            {
                if (IsOfficeHours())
                {
                    if (overflowTeam.Count > 0)
                    {
                        AssignChatToAgent(chatSession, overflowTeam);
                        return;
                    }
                    else
                    {
                        throw new Exception("Chat refused. Overflow team is not available.");
                    }
                }
                else
                {
                    throw new Exception("Chat refused. Queue is full and it's outside office hours.");
                }
            }

            queue.Enqueue(chatSession);
            currentQueueLength++;

            AssignChatToAgent(chatSession, agents);
        }

        public ChatSession GetChatSession(Guid sessionId)
        {
            return queue.FirstOrDefault(chatSession => chatSession.SessionId == sessionId);
        }

        public void EndShift()
        {
            // Wait for ongoing chats to finish
            while (agents.Any(agent => agent.CurrentChats > 0))
            {
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }

            agents.Clear();
            currentQueueLength = 0;
            maxQueueLength = 0;
            overflowTeam.Clear();
        }

        private void AssignChatToAgent(ChatSession chatSession, List<Agent> availableAgents)
        {
            Agent agent = GetNextAvailableAgent(availableAgents);
            if (agent != null)
            {
                agent.CurrentChats++;
                chatSession.AssignedAgent = agent;
                Console.WriteLine($"Assigned ChatSession to Agent. Agent Capacity: {agent.Capacity}, Current Chats: {agent.CurrentChats}");
            }
            else
            {
                throw new Exception("No available agents to assign the ChatSession.");
            }
        }

        private Agent GetNextAvailableAgent(List<Agent> availableAgents)
        {
            foreach (Agent agent in availableAgents)
            {
                if (agent.CurrentChats < agent.Capacity)
                {
                    return agent;
                }
            }

            return null;
        }

        private bool IsOfficeHours()
        {
            // Implement the logic for checking office hours
            // You can use DateTime.Now or any other approach based on your requirements
            return true;
        }
    }
}
