using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}