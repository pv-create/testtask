using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface ITodoService
    {
        List<TodoItem> GetTodoItems();
    }
}