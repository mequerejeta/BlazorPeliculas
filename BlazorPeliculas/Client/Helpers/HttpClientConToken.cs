using System.Net.Http;

namespace BlazorPeliculas.Client.Helpers
{
    public class HttpClientConToken
    {
        public HttpClientConToken(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}