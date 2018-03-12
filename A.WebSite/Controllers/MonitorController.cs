using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace A.WebSite.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MonitorController : Controller
    {
        private readonly IHubContext<MonitorHub> _hubContext;

        public MonitorController(IHubContext<MonitorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Broadcast([FromBody] BroadcastRequest request)
        {
            var now = DateTimeOffset.UtcNow;
            _hubContext.Clients.All.SendAsync("send", now, request.System, request.Message);

            return Ok($"Broadcast performed {now}/{request.System}");
        }
    }
}