﻿using HomeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask.Service
{
    public interface IChat
    {
        bool IsAgentActive(Agent agent);
        bool IsOfficeHours();
        int GetMaxQueueSize();
    }

    public class Chat : IChat
    {
        public bool IsAgentActive(Agent agent)
        {
            return true;
        }

        public bool IsOfficeHours()
        {
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