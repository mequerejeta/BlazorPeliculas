using System.Net.Http;

namespace BlazorPeliculas.Client.Helpers
{
    public class HttpClientSinToken
    {
        public HttpClientSinToken(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}