using Microsoft.EntityFrameworkCore;
using Todo.Core.Contracts;
using Todo.Core.Models;

namespace Todo.Infrustructure.Repositories
{
    public class TodoRepository : ItodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoRepository(TodoDbContext todoDbContext)
        {
            _context = todoDbContext;
        }
        public async Task<TodoItem> CreateTodoAsync(TodoItem todoItem)
        {
            var result = await _context.TodoItems.AddAsync(todoItem);

            _context.SaveChanges();

            return todoItem;
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetTodoItemsAsync()
        {
            var result = await _context.TodoItems.ToListAsync();

            return result;
        }
    }
}