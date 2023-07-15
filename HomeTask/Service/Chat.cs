using HomeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask.Service
{
    public class Chat
    {
        public bool IsAgentActive(Agent agent)
        {
            // Implement your logic to check if the agent is currently active
            // This can be based on shift timings or any other criteria you define
            return true;
        }

        public bool IsOfficeHours()
        {
            // Implement your logic to check if it's office hours
            // Return true if it's within office hours, otherwise false
            return true;
        }

        public int GetMaxQueueSize()
        {
            var teamACapacity = GetTeamCapacity(Objects.teamA);
            var teamBCapacity = GetTeamCapacity(Objects.teamB);
            var teamCCapacity = GetTeamCapacity(Objects.teamC);
            var overflowCapacity = Objects.overflowTeam.Count * 4;

            var capacity = teamACapacity + teamBCapacity + teamCCapacity + overflowCapacity;
            return capacity * Objects.MaxQueueSize;
        }

        private int GetTeamCapacity(List<Agent> team)
        {
            var concurrentChats = team.Sum(agent => (int)(agent.Capacity * GetAgentEfficiency(agent.Seniority)));
            return Math.Min(concurrentChats, Objects.MaxConcurrentChatsPerAgent);
        }

        private double GetAgentEfficiency(AgentSeniority seniority) => Objects.seniorityMultipliers[seniority];
    }
}