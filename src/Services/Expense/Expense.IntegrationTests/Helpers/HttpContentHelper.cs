using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Expense.IntegrationTests.Extensions
{
    public class HttpContentHelper<T>
    {
        public static async Task<T> ReadResult(HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadFromJsonAsync<T>().Result;
        }
    }
}
