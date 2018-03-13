using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace A.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IConfiguration _configuration;
        private static readonly HttpClient HttpClient = new HttpClient();

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Post([FromBody] SystemStatusRequest request)
        {
            var url = _configuration["ReportingUrl"];
            var serializeObject = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            HttpClient.PostAsync(url, new StringContent(serializeObject, Encoding.UTF8, "application/json"));

            return Ok(url);
        }
    }
}