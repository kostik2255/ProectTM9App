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
            // Проверка на null и пустые строки
            if (request == null ||
                string.IsNullOrEmpty(request.Name) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.PhoneNumber) ||
                string.IsNullOrEmpty(request.Question))
            {
                return BadRequest("Неверные данные запроса.");
            }

            // Установка даты и времени создания заявки
            request.CreatedAt = DateTime.UtcNow;
            request.IsCompleted = false; // Новая заявка не выполнена

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

        [HttpPut("{id}")]
        public IActionResult UpdateRequestStatus(int id)
        {
            var requests = LoadRequestsFromFile();
            var request = requests.FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return NotFound("Заявка не найдена.");
            }

            request.IsCompleted = true; // Установка статуса "выполнено"

            SaveRequestsToFile(requests); // Сохранение обновленного списка

            return Ok("Статус заявки обновлен.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRequest(int id)
        {
            var requests = LoadRequestsFromFile();
            var requestToRemove = requests.FirstOrDefault(r => r.Id == id);

            if (requestToRemove == null)
            {
                return NotFound("Заявка не найдена.");
            }

            requests.Remove(requestToRemove);

            SaveRequestsToFile(requests); // Сохранение обновленного списка

            return Ok("Заявка удалена.");
        }

        private void SaveRequestsToFile(List<ConsultationRequest> requests)
        {
            var json = JsonSerializer.Serialize(requests);
            System.IO.File.WriteAllText(FilePath, json);
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
                // Если произошла ошибка десериализации, возвращение пустого списка
                return new List<ConsultationRequest>();
            }
        }
    }

}
