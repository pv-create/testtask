using Todo.Core.Dtos;
using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Task<IReadOnlyCollection<TodoItem>> GetTodoItems();

        Task<TodoItem> CreateTodoItemAsync(TodoDto item);
    }
}