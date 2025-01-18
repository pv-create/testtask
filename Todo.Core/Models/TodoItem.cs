using Todo.Core.Enums;

namespace Todo.Core.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set;} = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set;}
        public TodoStatus TodoStatus { get; set; } = TodoStatus.InWork;
        public ICollection<TodoFile> Files { get; set; } = [];
    }
}