using Microsoft.AspNetCore.Mvc;
using ProectTM9Api.Models;
using System.Text.Json;

namespace ProectTM9Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private const string FilePath = "consultations.json";

        [HttpPost]
        public IActionResult SubmitRequest([FromBody] ConsultationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.PhoneNumber))
            {
                return BadRequest("Неверные данные запроса.");
            }

            // Сохранение данных в JSON файл
            SaveRequestToFile(request);

            return Ok("Запрос успешно отправлен.");
        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            var requests = LoadRequestsFromFile();
            return Ok(requests);
        }

        private void SaveRequestToFile(ConsultationRequest request)
        {
            try
            {
                var requests = LoadRequestsFromFile();
                requests.Add(request);
                var json = JsonSerializer.Serialize(requests);
                System.IO.File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения запроса в файл: {ex.Message}");
                throw;
            }
        }

        private List<ConsultationRequest> LoadRequestsFromFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return new List<ConsultationRequest>();
            }

            var json = System.IO.File.ReadAllText(FilePath);

            try
            {
                return JsonSerializer.Deserialize<List<ConsultationRequest>>(json) ?? new List<ConsultationRequest>();
            }
            catch (JsonException)
            {
                // Если произошла ошибка десериализации, возвращаем пустой список
                return new List<ConsultationRequest>();
            }

        }
    }
}
