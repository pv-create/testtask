using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Core.Services
{
    public class TodoService : ITodoService
    {
        public List<TodoItem> GetTodoItems()
        {
            var todoItems = new List<TodoItem>()
            {
                new TodoItem(),
                new TodoItem(),
                new TodoItem()
            };

            return todoItems;
        }
    }
}