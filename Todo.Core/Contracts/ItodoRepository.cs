using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Models;

namespace Todo.Core.Contracts
{
    public interface ItodoRepository
    {
        Task<TodoItem> CreateTodoAsync(TodoItem todoItem);
    }
}