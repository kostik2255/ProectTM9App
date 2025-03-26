using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ProectTM9Api.Models;
using System.Text.Json;
using System.Xml;

namespace ProectTM9Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private const string FilePath = "reviews.json";

        [HttpPost]
        //public IActionResult AddReview([FromBody] Review newReview)
        //{
        //    var reviews = LoadReviewsFromFile();

        //    newReview.Id = GetNextId(reviews); // Присваиваем новый ID
        //    reviews.Add(newReview);

        //    //SaveReviewToFile(reviews);

        //    //return CreatedAtAction(nameof(GetReviewById), new { id = newReview.Id }, newReview);
        //}

        private int GetNextId(List<Review> reviews)
        {
            if (reviews == null || !reviews.Any())
            {
                return 1; // Если нет отзывов, начинаем с 1
            }

            return reviews.Max(r => r.Id) + 1; // Возвращаем максимальный ID + 1
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = LoadReviewsFromFile();
            return Ok(reviews);
        }

        private void SaveReviewToFile(Review review)
        {
            try
            {
                var reviews = LoadReviewsFromFile();
                reviews.Add(review);
                var json = JsonSerializer.Serialize(reviews);
                System.IO.File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения отзыва в файл: {ex.Message}");
                throw; 
            }


        }

        private List<Review> LoadReviewsFromFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return new List<Review>();
            }

            var json = System.IO.File.ReadAllText(FilePath);

            try
            {
                return JsonSerializer.Deserialize<List<Review>>(json) ?? new List<Review>();
            }
            catch (JsonException)
            {
                // Если произошла ошибка десериализации, возвращаем пустой список
                return new List<Review>();
            }
        }

        private bool IsValidReview(Review review)
        {
            // Проверяем, что все обязательные поля заполнены
            return !string.IsNullOrWhiteSpace(review.FullName) &&
                   !string.IsNullOrWhiteSpace(review.Feedback) &&
                   !string.IsNullOrWhiteSpace(review.Company) &&
                   !string.IsNullOrWhiteSpace(review.Position);
        }

        [HttpPut("{id}")]
        public IActionResult PublishReview(int id)
        {
            var reviews = LoadReviewsFromFile();
            var review = reviews.FirstOrDefault(r => r.Id == id); // Предполагаем, что у вас есть уникальный идентификатор отзыва

            if (review == null)
            {
                return NotFound("Отзыв не найден.");
            }

            review.IsPublished = true;
            /*SaveReviewToFile(reviews);*/ // Сохраняем обновленный список отзывов

            return Ok("Отзыв успешно опубликован.");
        }
    }
}
