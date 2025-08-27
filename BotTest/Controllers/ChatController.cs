using BotTest.Data;
using BotTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotTest.Controllers
{
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
        public ActionResult<ChatResponse> SendMessage([FromBody] ChatRequest request)
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
            _context.SaveChanges();
            return Ok(new ChatResponse { Reply = reply });
        }
        [HttpGet("history")]
        public ActionResult<IEnumerable<ChatHistory>> GetHistory()
        {
            return Ok(_context.ChatHistories.OrderByDescending(x=>x.CreatedAt).ToList());
        }
    }
}
