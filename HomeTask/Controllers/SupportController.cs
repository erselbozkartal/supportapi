using HomeTask.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly SupportQueue supportQueue;
        private readonly MonitorService monitorService;

        public SupportController()
        {
            supportQueue = new SupportQueue();
            monitorService = new MonitorService(supportQueue);
        }

        [HttpPost("ChatRequest")]
        public IActionResult ChatRequest()
        {
            var chatSession = new ChatSession();
            supportQueue.EnqueueChatSession(chatSession);

            return Ok();
        }

        [HttpGet("ChatStatus/{sessionId}")]
        public IActionResult ChatStatus(Guid sessionId)
        {
            var chatSession = supportQueue.GetChatSession(sessionId);
            if (chatSession == null)
            {
                return NotFound();
            }

            chatSession.PollCount++;
            if (chatSession.PollCount >= 3)
            {
                chatSession.IsActive = false;
            }

            return Ok(chatSession);
        }

        [HttpPost("EndShift")]
        public IActionResult EndShift()
        {
            monitorService.StopMonitoring();
            supportQueue.EndShift();

            return Ok();
        }
    }
}
