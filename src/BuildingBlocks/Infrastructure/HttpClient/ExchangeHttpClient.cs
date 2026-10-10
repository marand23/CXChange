using System.Text;
using System.Text.Json;

namespace CXChange.BuildingBlocks.Infrastructure.HttpClient
{

    internal class ExchangeHttpClient : IExchangeHttpClient
    {
        IHttpClientFactory _client;
        Serilog.ILogger _logger;
        public ExchangeHttpClient(IHttpClientFactory client, Serilog.ILogger logger)
        {
            _client = client;
            _logger = logger;
        }


        public async Task<HttpResult<TResponse>> Execute<TRequest, TResponse>(TRequest content, Dictionary<string, string> headers, string url, HttpMethod method)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, url);
                if (headers != null)
                {
                    //todo : add headers for jbibit in client
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
                        return HttpResult<TResponse>.CreateSuccess(deserialized, "success");
                    }

                }

                var error = await response.Content.ReadAsStringAsync();
                return HttpResult<TResponse>.CreateFailure(error, HttpResultStatus.NotAccessable);
            }
            catch (HttpRequestException ex)
            {

                return HttpResult<TResponse>.CreateFailure("http requst failed", HttpResultStatus.NotAccessable);
            }
            catch (TimeoutException ex)
            {

                return HttpResult<TResponse>.CreateFailure("timeout exception", HttpResultStatus.NotAccessable);
            }
            catch (Exception ex)
            {

                return HttpResult<TResponse>.CreateFailure("global exception occured", HttpResultStatus.NotAccessable);
            }




        }
    }
}
