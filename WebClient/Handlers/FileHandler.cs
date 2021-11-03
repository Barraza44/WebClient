using System.IO;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Handlers
{
    public class FileHandler : IFileService 
    {
        public async Task SaveAsync(string path)
        {
            
            
        }

        public Task LoadAsync(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}