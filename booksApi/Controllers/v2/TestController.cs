using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")] 
    //[Route("api/v{version:apiVersion}/[controller]")] // *** this uses route to specify api version
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("TestController V2");
        }
    }
}
