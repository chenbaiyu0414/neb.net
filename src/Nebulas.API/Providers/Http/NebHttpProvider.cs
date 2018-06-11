using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nebulas.API.Providers.Http
{
    internal sealed class NebHttpProvider : IProvider
    {
        public string Host { get; }

        public string ApiVersion { get; set; }

        public string ApiDomain { get; set; }

        private readonly HttpClient m_httpClient;

        public NebHttpProvider(string host, string apiVersion, string apiDomain, int timeout = 30)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("host is empty");

            Host = host;
            ApiVersion = apiVersion;
            ApiDomain = apiDomain;

            m_httpClient = new HttpClient {Timeout = TimeSpan.FromSeconds(timeout)};
        }

        public async Task<TResult> SendRequest<TResult>(HttpMethod method, string apiName, object payload = null)
        {
            if (!(method == HttpMethod.Get || method == HttpMethod.Post))
                throw new NotSupportedException("method must be GET or POST");

            var payloadString = payload == null ? string.Empty : JsonConvert.SerializeObject(payload);

            var url = $"{Host}/{ApiVersion}/{ApiDomain}/{apiName}";

            using (var request = new HttpRequestMessage(method, url))
            {
                if (method == HttpMethod.Post)
                    request.Content = new StringContent(payloadString, Encoding.UTF8, "application/json");

                using (var response = await m_httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<NebHttpResponse<TResult>>(responseContent).Result;
                }
            }
        }
    }
}
