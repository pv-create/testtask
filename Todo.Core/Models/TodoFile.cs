namespace Todo.Core.Models
{
    public class TodoFile
    {
        public Guid Id { get; set; }
        public string Name { get; set;} = string.Empty;
        public string Folder { get; set; } = string.Empty;

        public Guid TodoId { get; set; }
    }
}