using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.Helpers;

namespace TVSeriesAPI.Controllers
{
    [Route("/errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("{code}")]
        public IActionResult Error(int code)
        {
            //var parsedCode = (HttpStatusCode)code;
            //var error = new ApiError(code, parsedCode.ToString());

            //return new ObjectResult(error);

            return StatusCode(code, new JsonMessage<string>()
            {
                Success = false,
                ErrorMessage = "Oops!!! Something went wrong!"
            });
        }
    }
}
