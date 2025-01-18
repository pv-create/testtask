using Todo.Core.Dtos;
using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface ITodoService
    {
        List<TodoItem> GetTodoItems();

        Task<TodoItem> CreateTodoItemAsync(TodoDto item);
    }
}