using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Handlers
{
    public class FileHandler : IFileService
    {
        public async Task SaveAsync(string path, string data)
        {
            await using var fs = new FileStream(path, FileMode.OpenOrCreate);
            var byteData = Encoding.UTF8.GetBytes(data);
            await fs.WriteAsync(byteData);
        }

        public async Task<(string, string)> LoadAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found; no such file or directory");
            var fileType = new FileInfo(path).Extension;
            await using var fs = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(fs);
            return (await reader.ReadToEndAsync(), fileType);
        }

        public async Task SaveAsync(string path, byte[] data)
        {
            await using var fs = new FileStream(path, FileMode.CreateNew);
            await fs.WriteAsync(data);
        }
    }
}