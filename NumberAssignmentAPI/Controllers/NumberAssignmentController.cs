using Microsoft.AspNetCore.Mvc;
using BusinessLogic.NumberAssignment;

namespace NumberAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberAssignmentController : ControllerBase
    {        
        private readonly NumberAssigment _businessLogic;

        public NumberAssignmentController(NumberAssigment businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [HttpGet]
        public IActionResult GetNumber(int clientId, int userId, int raffleId)
        {
            var myRandomNumber = _businessLogic.GetNumber(clientId, userId, raffleId);

            return Ok(myRandomNumber);
        }
    }
}
