using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Core.Interfaces
{
    public interface IFileService
    {
        Task UploadFile(string filePath, Stream content, Guid todoId);
        Task DownloadFile (Guid fileId);
    }
}