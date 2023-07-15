using HomeTask.Models;
using HomeTask.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeTask.Controllers
{
    [Route("api/support-requests")]
    [ApiController]
    public class SupportRequestsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateSupportRequest()
        {
            var chat = new Chat();
            if ((Objects._chatQueue.Count >= Objects.MaxQueueSize && !chat.IsOfficeHours()) || (Objects._chatQueue.Count >= chat.GetMaxQueueSize() && chat.IsOfficeHours()))
                return BadRequest("Chat request cannot be accepted at the moment. Please try again later.");
            
            var chatSession = new ChatSession
            {
                RequestId = Guid.NewGuid().ToString(),
                SessionDate = DateTime.Now,
                Status = "Queued",
                PollCount = 0,
                LastPolledAt = DateTime.Now
            };
            Objects._chatQueue.Enqueue(chatSession);
            return Ok(chatSession);
        }

        [HttpGet("{requestId}")]
        public IActionResult GetChatSession(string requestId)
        {
            if (!Objects._activeChatSessions.ContainsKey(requestId))
                return NotFound();
            var chatSession = Objects._activeChatSessions[requestId];
            chatSession.PollCount++;
            chatSession.LastPolledAt = DateTime.Now;
            if (chatSession.PollCount >= 3)
            {
                chatSession.Status = "Inactive";
            }
            return Ok(chatSession);
        }
    }
}