using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SMBLibrary;
using SMBLibrary.Client;
using Todo.Core.Contracts;
using Todo.Core.Options;

namespace Todo.Infrustructure.StorageServices
{
    public class SambaService : ISambaService
    {
        private readonly SambaOptions _options;
        private readonly ILogger<SambaService> _logger;

        public SambaService(IOptions<SambaOptions> options, ILogger<SambaService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        private SMB2Client GetClient()
        {
            try
            {
                _logger.LogInformation("Attempting to connect to {Server}", _options.Server);

                var client = new SMB2Client();
                bool isConnected = client.Connect(_options.Server, SMBTransportType.DirectTCPTransport);

                _logger.LogInformation("Connection result: {IsConnected}", isConnected);

                if (!isConnected)
                    throw new Exception("Failed to connect to server");

                _logger.LogInformation("Attempting to login with Username: {Username}, Domain: {Domain}",
                    _options.Username, _options.Domain);

                NTStatus status = client.Login(_options.Domain, _options.Username, _options.Password);

                _logger.LogInformation("Login status: {Status}", status);

                if (status != NTStatus.STATUS_SUCCESS)
                    throw new Exception($"Login failed: {status}");

                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create SMB client");
                throw new Exception("Failed to create SMB client", ex);
            }
        }

        public Task DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ListFilesAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> ReadFileAsync(string filePath)
        {
            SMB2Client client = GetClient();
            ISMBFileStore fileStore = client.TreeConnect(_options.ShareName, out NTStatus status);

            if (status != NTStatus.STATUS_SUCCESS)
                throw new Exception("Failed to connect to share");

            object handle;
            status = fileStore.CreateFile(
                out handle,
                out FileStatus fileStatus,
                filePath,
                AccessMask.GENERIC_READ,
                SMBLibrary.FileAttributes.Normal,
                ShareAccess.Read,
                CreateDisposition.FILE_OPEN,
                CreateOptions.FILE_NON_DIRECTORY_FILE,
                null);

            if (status != NTStatus.STATUS_SUCCESS)
                throw new Exception($"Failed to open file: {filePath}");

            var memoryStream = new MemoryStream();
            byte[] buffer = new byte[4096];
            long offset = 0;

            while (true)
            {
                status = fileStore.ReadFile(out byte[] data, handle, offset, 4096);
                if (status != NTStatus.STATUS_SUCCESS || data.Length == 0)
                    break;

                await memoryStream.WriteAsync(data);
                offset += data.Length;
            }

            fileStore.CloseFile(handle);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task WriteFileAsync(string filePath, Stream content)
        {
            try
            {
                var client = GetClient();
                ISMBFileStore fileStore = client.TreeConnect(_options.ShareName, out NTStatus status);

                if (status != NTStatus.STATUS_SUCCESS)
                    throw new Exception($"Failed to connect to share: {status}");

                // Создаем или перезаписываем файл
                object handle;
                status = fileStore.CreateFile(
                    out handle,
                    out FileStatus fileStatus,
                    filePath,
                    AccessMask.GENERIC_WRITE,
                    SMBLibrary.FileAttributes.Normal,
                    ShareAccess.None,
                    CreateDisposition.FILE_OVERWRITE_IF,
                    CreateOptions.FILE_NON_DIRECTORY_FILE,
                    null);

                if (status != NTStatus.STATUS_SUCCESS)
                    throw new Exception($"Failed to create file {filePath}: {status}");

                try
                {
                    const int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];
                    long offset = 0;

                    while (true)
                    {
                        int bytesRead = await content.ReadAsync(buffer, 0, bufferSize);
                        if (bytesRead == 0)
                            break;

                        if (bytesRead < bufferSize)
                        {
                            // Для последнего chunk'а если он меньше размера буфера
                            var lastChunk = new byte[bytesRead];
                            Array.Copy(buffer, lastChunk, bytesRead);
                            int bytesWritten;
                            status = fileStore.WriteFile(out bytesWritten, handle, offset, lastChunk);
                        }
                        else
                        {
                            int bytesWritten;
                            status = fileStore.WriteFile(out bytesWritten, handle, offset, buffer);
                        }

                        if (status != NTStatus.STATUS_SUCCESS)
                            throw new Exception($"Failed to write to file at offset {offset}: {status}");

                        offset += bytesRead;
                    }

                    // Сбрасываем буферы
                    status = fileStore.FlushFileBuffers(handle);
                    if (status != NTStatus.STATUS_SUCCESS)
                        _logger.LogWarning("Failed to flush file buffers: {Status}", status);
                }
                finally
                {
                    // Всегда закрываем файл
                    status = fileStore.CloseFile(handle);
                    if (status != NTStatus.STATUS_SUCCESS)
                        _logger.LogWarning("Failed to close file handle: {Status}", status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing file {FilePath}", filePath);
                throw new Exception($"Error writing file: {filePath}", ex);
            }
        }
    }
}