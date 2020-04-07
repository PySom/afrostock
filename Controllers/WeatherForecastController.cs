using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AfrroStock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        
        [HttpGet("ext")]
        public async Task<IActionResult> GetAll()
        {
            HttpClient client = new HttpClient();
            var path = @"http://infomall-001-site1.etempurl.com/api/courses/all";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(res);
            }
            return Ok();
        }
    }
}
