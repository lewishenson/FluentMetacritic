using System.Threading.Tasks;

namespace FluentMetacritic.Net
{
    public interface IHttpClient
    {
        Task<string> GetContentAsync(string address);
    }
}