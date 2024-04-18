using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NumberAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberAssignmentController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetNumber()
        {
            var myRandomNumber = new Random().Next();

            return Ok(myRandomNumber);
        }
    }
}
