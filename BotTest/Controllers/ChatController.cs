using BotTest.Data;
using BotTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task< ActionResult<ChatResponse>> SendMessage([FromBody] ChatRequest request)
        {
            string reply;
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                reply = "Zehmet olmasa mesaj daxil edin.";
            }
            else if (request.Message.ToLower().Contains("salam"))
            {
                reply = "Salam! Necesen? ";
            }
            else if (request.Message.ToLower().Contains("sag ol"))
            {
                reply = "Xosdur ! Goruserik! ";
            }
            else
            {
                reply = "Uzr isteyirem,sizi tam basa dusmedim!";
            }
            var history = new ChatHistory
            {
                UserMessage = request.Message,
                BotReply = reply
            };
            _context.ChatHistories.Add(history);
            _context.SaveChangesAsync();
            return Ok(new ChatResponse { Reply = reply });
        }
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<ChatHistory>>> GetHistory()
        {
            var history = await _context.ChatHistories
                                        .OrderByDescending(x => x.CreatedAt)
                                        .ToListAsync();
            return Ok(history);
        }

    }
}
