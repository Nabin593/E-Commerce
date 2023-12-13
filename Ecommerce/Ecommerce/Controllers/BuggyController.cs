using Ecommerce.Data;
using Ecommerce.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("NOT Found")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var thing = _storeContext.products.Find(42);

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var thingsToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("Badrequest")]
        public ActionResult GetBadRequest()
        {

            return BadRequest( new ApiResponse(404));
        }

        [HttpGet("Badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
