using System.Net.Http;

namespace AttendEdgeWebConsole.Infrastructure
{
    public class HttpClientProvider
    {
        private static readonly HttpClient _client = new HttpClient();

        public static HttpClient Client => _client;
    }
}
