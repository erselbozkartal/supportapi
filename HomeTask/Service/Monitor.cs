using HomeTask.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HomeTask.Service
{
    public class Monitor
    {
        public void ChatQueue()
        {
            var chat = new Chat();
            while (true)
            {
                if (Objects._chatQueue.Count > 0)
                {
                    var chatSession = Objects._chatQueue.Peek();
                    if ((chatSession.Status == "Queued" && chat.IsOfficeHours()) || (chatSession.Status == "Queued" && Objects._activeChatSessions.Count < chat.GetMaxQueueSize()))
                    {
                        AssignChatSessionToAgent(chatSession);
                        Objects._chatQueue.Dequeue();
                    }
                }

                foreach (var activeSession in Objects._activeChatSessions.Values.ToList())
                {
                    if (activeSession.PollCount >= 3)
                    {
                        activeSession.Status = "Inactive";
                        Objects._activeChatSessions.Remove(activeSession.RequestId);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void AssignChatSessionToAgent(ChatSession chatSession)
        {
            if (Objects.teamA.Count > 0)
                AssignChatSessionToAgentInTeam(chatSession, Objects.teamA, ref Objects.teamAIndex);

            else if (Objects.teamB.Count > 0)
                AssignChatSessionToAgentInTeam(chatSession, Objects.teamB, ref Objects.teamBIndex);

            else if (Objects.teamC.Count > 0)
                AssignChatSessionToAgentInTeam(chatSession, Objects.teamC, ref Objects.teamCIndex);

            else if (Objects.overflowTeam.Count > 0)
                AssignChatSessionToAgentInTeam(chatSession, Objects.overflowTeam, ref Objects.overflowIndex);
        }

        private void AssignChatSessionToAgentInTeam(ChatSession chatSession, List<Agent> team, ref int teamIndex)
        {
            var seniorityOrder = new AgentSeniority[] { AgentSeniority.Junior, AgentSeniority.MidLevel, AgentSeniority.Senior, AgentSeniority.TeamLead };

            foreach (var seniority in seniorityOrder)
            {
                var agentsWithSeniority = team.Where(agent => agent.Seniority == seniority).ToList();
                if (agentsWithSeniority.Any())
                {
                    var agentIndex = teamIndex % agentsWithSeniority.Count;
                    var agent = agentsWithSeniority[agentIndex];

                    chatSession.Status = $"Assigned to {agent.Name}";
                    agent.Capacity--;

                    if (agent.Capacity == 0)
                    {
                        team.Remove(agent);
                        teamIndex--;
                    }

                    Objects._activeChatSessions.Add(chatSession.RequestId, chatSession);
                    teamIndex++;
                    break;
                }
            }
        }
    }
}