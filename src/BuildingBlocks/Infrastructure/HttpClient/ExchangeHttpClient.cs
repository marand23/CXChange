using System.Text;
using System.Text.Json;

namespace CXChange.BuildingBlocks.Infrastructure.HttpClient
{

    internal class ExchangeHttpClient : IExchangeHttpClient
    {
        IHttpClientFactory _client;
        public ExchangeHttpClient(IHttpClientFactory client)
        {
            _client = client;
        }


        public async Task<TResponse> Execute<TRequest, TResponse>(TRequest content, Dictionary<string, string> headers, string url, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (content != null)
            {
                StringContent contents = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                request.Content = contents;
            }

            var response = await _client.CreateClient().SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    var deserialized = await JsonSerializer.DeserializeAsync<TResponse>(contentStream);
                    if (deserialized == null)
                    {
                        ArgumentNullException.ThrowIfNull(nameof(deserialized));
                    }
                    return deserialized;
                }

            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}
