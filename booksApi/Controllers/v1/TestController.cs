using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.9")]

    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")] // *** this uses route to specify api version
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("TestController V1");
        }

        // *** how to version endpoint 
        [HttpGet("get-test-data"), MapToApiVersion("1.9")]
        public IActionResult GetV19()
        {
            return Ok("TestController V1.9");
        }



    }
}
