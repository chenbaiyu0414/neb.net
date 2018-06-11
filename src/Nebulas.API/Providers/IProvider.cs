using System.Net.Http;
using System.Threading.Tasks;

namespace Nebulas.API.Providers
{
    internal interface IProvider
    {
        Task<TResult> SendRequest<TResult>(HttpMethod method, string apiName, object payload = null);
    }
}