using Microsoft.AspNetCore.Mvc;
using ProectTM9Api.Models;

namespace ProectTM9Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private static List<ConsultationRequest> _requests = new List<ConsultationRequest>();

        [HttpPost]
        public IActionResult SubmitRequest([FromBody] ConsultationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.PhoneNumber))
            {
                return BadRequest("Invalid request data.");
            }

            // Сохраните данные в список (или в базу данных)
            _requests.Add(request);

            return Ok("Request submitted successfully.");
        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            return Ok(_requests);
        }
    }
}
