namespace CXChange.BuildingBlocks.Infrastructure.HttpClient
{
    public interface IExchangeHttpClient
    {
        Task<TResponse> Execute<TRequest, TResponse>(TRequest content, Dictionary<string, string> headers, string url, HttpMethod method);
    }
}
