using Todo.Core.Contracts;
using Todo.Core.Dtos;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly ItodoRepository _repository;

        public TodoService(ItodoRepository itodoRepository)
        {
            _repository = itodoRepository;
        }
        public async Task<TodoItem> CreateTodoItemAsync(TodoDto item)
        {
            TodoItem newTodo = new(){
                Id = Guid.NewGuid(),
                Name = item.Name,
            };

            var result = await _repository.CreateTodoAsync(newTodo);

            return result;
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetTodoItems()
        {
            var result = await _repository.GetTodoItemsAsync();

            return result;
        }
    }
}