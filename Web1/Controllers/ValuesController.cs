using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory != null) _httpClientFactory = httpClientFactory;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            string[] values;
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("http://localhost:8277/api/values");
                response.EnsureSuccessStatusCode();
                values = JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
            }

            return values;
        }
    }
}
