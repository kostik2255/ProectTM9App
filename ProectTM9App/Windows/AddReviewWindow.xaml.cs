using ProectTM9Api.Models;
using ProectTM9App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProectTM9App.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddReviewWindow.xaml
    /// </summary>
    public partial class AddReviewWindow : Window
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public AddReviewWindow()
        {
            InitializeComponent();
        }

        private async void AddReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            var review = new Review
            {
                FullName = NameTextBox.Text,
                Company = CompanyTextBox.Text,
                Position = PositionTextBox.Text,
                Feedback = ReviewTextBox.Text
            };

            if (string.IsNullOrWhiteSpace(review.FullName) || string.IsNullOrWhiteSpace(review.Feedback))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            await SubmitReview(review);
        }

        private async Task SubmitReview(Review review)
        {
            var json = JsonSerializer.Serialize(review);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://localhost:7157/api/Reviews", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Отзыв успешно отправлен.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при отправке отзыва: " + response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Ошибка сети: " + ex.Message);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Запрос превысил время ожидания.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

    }
}
