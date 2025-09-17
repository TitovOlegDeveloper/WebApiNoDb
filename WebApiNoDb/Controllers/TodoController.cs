using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiNoDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
        {
            var items = await _todoService.GetAllItemAsync();
            return Ok(items);
        }


        [HttpGet("{guid}")]
        public async Task<ActionResult<TodoItemDto>> GetById(Guid guid)
        {
            var item = await _todoService.GetItemByIdAsync(guid);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] TodoItemDto itemDto)
        {
            var createdItem = await _todoService.CreateItemAsync(itemDto);
            return CreatedAtAction(nameof(GetById), new { guid = createdItem.Guid }, createdItem);
        }


    }
}
