using Microsoft.EntityFrameworkCore;
using Todo.Core.Contracts;
using Todo.Core.Models;

namespace Todo.Infrustructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly TodoDbContext _context;
        public FileRepository(TodoDbContext todoDbContext)
        {
            _context = todoDbContext;
        }
        public async Task AddFile(TodoFile todoFile)
        {
            _context.TodoFiles.Add(todoFile);

            await _context.SaveChangesAsync();
        }

        public async Task<TodoFile> GetTodoFile(Guid id)
        {
            return await _context.TodoFiles.FirstAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<TodoFile>> GetTodoFilesAsync()
        {
            return await _context.TodoFiles.ToArrayAsync();
        }
    }
}