using System.ComponentModel;
using WebClient.Configurators;

namespace WebClient
{
    [TypeConverter(typeof(HttpMethodConverter))]
    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Patch,
        Delete
    }
}