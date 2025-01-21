using Microsoft.AspNetCore.Mvc;
using Todo.Core.Dtos;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService; 
        private readonly ILogger<TodoController> _logger;
        public TodoController(ITodoService todoService, ILogger<TodoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpPost]
        [Route(nameof(CreateTodo))]
        public async Task<IActionResult> CreateTodo([FromBody] TodoDto request)
        {
            _logger.LogInformation("ping");
            await _todoService.CreateTodoItemAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<TodoItem>>> GetTodos()
        {
            var todoItems = await _todoService.GetTodoItems();
            return Ok(todoItems);
        }
    }
}