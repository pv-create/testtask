namespace Todo.Core.Contracts
{
    public interface ISambaService
    {
        Task<Stream> ReadFileAsync(string filePath);

        Task WriteFileAsync(string filePath, Stream content);

        Task DeleteFileAsync(string filePath);

        Task<IEnumerable<string>> ListFilesAsync(string path);
    }
}