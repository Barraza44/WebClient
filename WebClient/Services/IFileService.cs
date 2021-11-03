using System.IO;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IFileService
    {
        public Task SaveAsync(string path);

        public Task LoadAsync(string path);
    }
}