using System.IO;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IFileService
    {
        public Task SaveAsync(string path, string data);

        public Task<(string, string)> LoadAsync(string path);
    }
}