using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetOpenTelemetryZipkinPromethues.Pages
{
    public class RequestsModel : PageModel
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        private readonly string BaseURL;

        public RequestsModel(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _HttpClientFactory = httpClientFactory;
            BaseURL = configuration["BaseUrl"];
        }

        public async Task OnGetAsync()
        {
            await MakeRequestAsync("http://www.pudim.com.br");

            await MakeParallelRequestsAsync();

            await MakeRequestAsync($"{BaseURL}api/Values");

            await MakeRequestAsync("http://www.pudim.com.br");
        }

        private async Task MakeParallelRequestsAsync()
        {
            var RequestTasks = new List<Task>();

            for(var I = 0; I < 15; I++)
            {
                RequestTasks.Add(MakeRequestAsync("http://www.pudim.com.br"));
            }

            await Task.WhenAll(RequestTasks);
        }

        private async Task MakeRequestAsync(string url)
        {
            var client = _HttpClientFactory.CreateClient("DefaultClient");

            var response = await client.GetAsync(url);

            await response.Content.ReadAsStringAsync();
        }
    }
}
