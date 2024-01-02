using Microsoft.AspNetCore.Mvc;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Services;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessageController : Controller
    {
        private readonly ChatMessageService _chatMessageService;

        public ChatMessageController(ChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<List<ChatMessage>> Get()
        {
            return await _chatMessageService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var chatMessage = await _chatMessageService.GetByIdAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return Ok(chatMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatMessage chatMessage)
        {
            await _chatMessageService.CreateAsync(chatMessage);
            return CreatedAtAction(nameof(Get), new { id = chatMessage.Id }, chatMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ChatMessage updatedChatMessage)
        {
            var existingChatMessage = await _chatMessageService.GetByIdAsync(id);

            if (existingChatMessage == null)
            {
                return NotFound();
            }

            await _chatMessageService.UpdateAsync(id, updatedChatMessage);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var chatMessage = await _chatMessageService.GetByIdAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            await _chatMessageService.DeleteAsync(id);

            return NoContent();
        }
    }
}
