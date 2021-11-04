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
            var stream = new MemoryStream(byteData);
            await stream.CopyToAsync(fs);
        }

        public Task LoadAsync(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}