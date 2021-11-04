using System;
using System.IO;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IPrintService
    {
        public void PrintData(string data);

        public void PrintError(Exception error);

        public Task PrintDataProgress(Stream dataStream);
    }
}