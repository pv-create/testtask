using Todo.Core.Contracts;
using Todo.Core.Interfaces;

namespace Todo.Core.Services
{
    public class FileService : IFileService
    {
        private readonly ISambaService _sambaService;
        private readonly IFileRepository _fileRepository;
        public FileService(ISambaService sambaService, IFileRepository fileRepository)
        {
            _sambaService = sambaService;
            _fileRepository = fileRepository;
        }

        public async Task DownloadFile(Guid fileId)
        {
            var file = await _fileRepository.GetTodoFile(fileId);

            
        }

        public async Task UploadFile(string filePath, Stream content, Guid todoId)
        {
            await _sambaService.WriteFileAsync(filePath, content);
        }
    }
}