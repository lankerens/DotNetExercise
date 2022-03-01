using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.controller
{

    [ApiController]
    [Route("/api/lankerens")]
    public class LankerensController : ControllerBase
    {

        
        // [fromuri]
        [HttpGet]
        public IActionResult getTest(string? str = null) {
            Console.WriteLine("str == " + str);

            JsonResult jr = new JsonResult(str);
            jr.StatusCode = 200;
            return Ok(jr);
        }



        // [formbody] => json
        // [fromfrom] => from-date
        [HttpPost]
        public IActionResult postTest(dynamic obj) {
            obj = JsonConvert.DeserializeObject(Convert.ToString(obj));
            string s = obj.str;
            Console.WriteLine("str == " + s);

            Console.WriteLine();

            JsonResult jr = new JsonResult(s);
            jr.StatusCode = 200;
            return Ok(jr);
        }



        // 和post相同
        [HttpPut]
        public IActionResult putTest(dynamic obj) {
            obj = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(obj));
            string s = obj.str;
            Console.WriteLine("str == " + s);

            JsonResult jr = new JsonResult(s);
            jr.StatusCode = 200;
            return Ok(jr);
        }


        // 和post差不多
        [HttpDelete]
        public IActionResult deleteTest(dynamic obj)
        {
            obj = JsonConvert.DeserializeObject(Convert.ToString(obj));
            string s = obj.str;
            Console.WriteLine("str == " + s);

            JsonResult jr = new JsonResult(s);
            jr.StatusCode = 200;
            return Ok(jr);
        }

    }
}
