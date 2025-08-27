using BotTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
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
            return Ok(new ChatResponse { Reply = reply });
        }
    }
}
