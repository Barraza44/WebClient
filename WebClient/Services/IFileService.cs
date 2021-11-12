using System.IO;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Opens a stream to save text-based data to a file asynchronously. If the file does not exist, it is created
        /// </summary>
        /// <param name="path">File on which the data will be saved</param>
        /// <param name="data">The data to save as text</param>
        /// <returns>A task representing an asynchronous operation</returns>
        public Task SaveAsync(string path, string data);

        /// <summary>
        /// Opens a stream to save data, as bytes, to a file asynchronously. If the file does not exist, it is created
        /// </summary>
        /// <param name="path">File on which the data will be saved</param>
        /// <param name="data">The data to save as bytes</param>
        /// <returns>A task representing an asynchronous operation</returns>
        public Task SaveAsync(string path, byte[] data);

        /// <summary>
        /// Opens a stream to load data, as text, from a file asynchronously. If the file does not exist, an exception is thrown
        /// </summary>
        /// <param name="path">File from which the data will be loaded</param>
        /// <returns>A tuple containing the file contents as a string, and its extension</returns>
        /// <exception cref="FileNotFoundException">No such file or directory exists</exception>
        public Task<(string, string)> LoadAsync(string path);
    }
}