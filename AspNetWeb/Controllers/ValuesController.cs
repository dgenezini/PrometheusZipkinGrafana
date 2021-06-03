using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult<string>> GetAsync()
        {
            var client = _HttpClientFactory.CreateClient("DefaultClient");

            var response = await client.GetAsync("http://www.pudim2.com.br");

            await response.Content.ReadAsStringAsync();

            return "ok";
        }
    }
}
