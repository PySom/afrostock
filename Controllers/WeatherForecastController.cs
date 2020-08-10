using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyMATEUpload.Models;

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
            List<Course> main = new List<Course>();
            var path = @"http://infomall-001-site1.etempurl.com/api/courses/";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<Course>>(res);
                foreach(var data in json)
                {
                    var testpath = $"http://infomall-001-site1.etempurl.com/api/courses/{data.Id}";
                    HttpResponseMessage responsePath = await client.GetAsync(testpath);
                    if (responsePath.IsSuccessStatusCode)
                    {
                        var res1 = await responsePath.Content.ReadAsStringAsync();
                        var json1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Course>(res1);
                        main.Add(json1);
                    }
                }
            }
            //var f = Newtonsoft.Json.JsonConvert.SerializeObject(main);
            //var d = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(f);
            return Ok(main);
        }
    }
}
