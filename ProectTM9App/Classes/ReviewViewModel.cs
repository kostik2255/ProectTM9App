using Newtonsoft.Json;
using ProectTM9Api.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProectTM9App.Classes
{
    public class ReviewViewModel
    {
        public ObservableCollection<Review> Reviews { get; set; }
        private const string ApiUrl = "https://localhost:7157/api/Reviews";

        public ReviewViewModel()
        {
            Reviews = new ObservableCollection<Review>();
            LoadReviews();
        }

        public async Task SubmitReview(Review review)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(review);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(ApiUrl, content);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    // Логирование ошибки
                    Console.WriteLine($"Request error: {e.Message}");
                    throw; // Или обработайте по-другому
                }
            }
        }

        public async Task LoadReviews()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiUrl);
                var reviews = JsonConvert.DeserializeObject<List<Review>>(response);
                if (reviews != null)
                {
                    Reviews.Clear();
                    foreach (var review in reviews)
                    {
                        Reviews.Add(review);
                    }
                }
            }
        }
    }
}
