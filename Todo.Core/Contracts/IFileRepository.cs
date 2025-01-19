using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Models;

namespace Todo.Core.Contracts
{
    public interface IFileRepository
    {
        Task<IReadOnlyCollection<TodoFile>> GetTodoFilesAsync();
        Task<TodoFile> GetTodoFile(Guid id);
        Task AddFile(TodoFile todoFile);
    }
}