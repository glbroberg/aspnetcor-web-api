using booksApi.ActionResults;
using booksApi.Data.Models;
using booksApi.Data.Services;
using booksApi.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : Controller
    {
        private PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                var publisherList = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(publisherList);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, an error occured getting publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddAuthor([FromBody] PublisherVM publisherVM)
        {
            _publishersService.AddPublisher(publisherVM);
            return Ok();
        }

        [HttpGet("get_publisher-data")]
        public IActionResult GetPublisherData(int publisherId)
        {
            var pubData = _publishersService.GetPublisherData(publisherId);
            return Ok(pubData);
        }

        // *** ActionResult<T> return type
        [HttpGet("get_publisher-by-id/{publisherId}")]
        public ActionResult<Publisher> GetPublisherById(int publisherId)
        {
            var _result = _publishersService.GetPublisherById(publisherId);

            if (_result != null)
            {
                //return Ok(pubData);
                return _result;
            }
            else
            {
                return NotFound();

            }
        }

        // *** Custom ActionResult
        [HttpGet("get_custom-publisher-by-id/{publisherId}")]
        public CustomActionResult CustomGetPublisherById(int publisherId)
        {
            var _result = _publishersService.GetPublisherById(publisherId);

            if (_result != null)
            {
                var _responseObject = new CustomActionResultVM
                {
                    Publisher = _result
                };

                return new CustomActionResult(_responseObject);
            }
            else
            {
                var _responseObject = new CustomActionResultVM
                {
                    Exception = new Exception("This is coming from publishers controller")
                };

                return new CustomActionResult(_responseObject);

            }
        }

        [HttpDelete("delete-publisher/{publisherId}")]
        public IActionResult DeletePublisherById(int publisherId)
        {
            _publishersService.DeletePublisherById(publisherId);
            return Ok();
        }
    }
}
