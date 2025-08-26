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
        private int GetNextId(List<ConsultationRequest> requests)
        {
            return requests.Count > 0 ? requests.Max(r => r.Id) + 1 : 1;
        }

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

            // Установка уникального идентификатора
            var requests = LoadRequestsFromFile();
            request.Id = GetNextId(requests);

            // Установка даты и времени создания заявки
            request.CreatedAt = DateTime.UtcNow;
            request.IsCompleted = false; // Новая заявка не выполнена

            // Сохранение данных в JSON файл
            SaveRequestToFile(request);

            return Ok("Запрос успешно отправлен.");
        }

        [HttpGet]
        public IActionResult GetRequests(bool? completed = null)
        {
            var requests = LoadRequestsFromFile();

            if (completed.HasValue)
            {
                requests = requests.Where(r => r.IsCompleted == completed.Value).ToList();
            }

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

        public class UpdateStatusRequest
        {
            public bool IsCompleted { get; set; }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRequestStatus(int id, [FromBody] UpdateStatusRequest updateStatusRequest)
        {
            var requests = LoadRequestsFromFile();
            var request = requests.FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return NotFound("Заявка не найдена.");
            }

            request.IsCompleted = updateStatusRequest.IsCompleted; // Установка статуса

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
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Отступы для читаемости
                };
                var json = JsonSerializer.Serialize(requests, options);
                System.IO.File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения запросов в файл: {ex.Message}");
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
                // Если произошла ошибка десериализации, возвращение пустого списка
                return new List<ConsultationRequest>();
            }
        }
    }

}
