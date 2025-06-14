using ProectTM9Api.Models;
using ProectTM9App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProectTM9App.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private RequestTracker requestTracker = new RequestTracker();
        public MainPage()
        {
            InitializeComponent();
        }


        private async void GetConsultationBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = textBoxName.Text;
            var email = textBoxEmail.Text;
            var phoneNumber = textBoxNumber.Text;
            var question = textBoxQuestion.Text;

            // Валидация данных
            if (!IsValidName(name))
            {
                MessageBox.Show("Введите корректное имя.");
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введите корректный Email.");
                return;
            }
            if (!IsValidPhoneNumber(phoneNumber))
            {
                MessageBox.Show("Номер телефона должен начинаться на '+'.");
                return;
            }
            if (!IsValidQuestion(question))
            {
                MessageBox.Show("Вопрос не может быть пустым (или введите корректный вопрос)");
                return;
            }

            if (!requestTracker.CanSendRequest())
            {
                MessageBox.Show("Вы достигли лимита на отправку заявок. Пожалуйста, подождите.");
                return;
            }

            var request = new ConsultationRequest
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Question = question
            };

            using (var httpClient = new HttpClient())
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync("https://localhost:7157/api/Consultation", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Запрос успешно отправлен.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при отправке запроса. Код статуса: " + response.StatusCode);
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

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name != "Ваше имя*";
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains('@');
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Проверка, что номер начинается с "+" и длина номера больше 1
            if (phoneNumber.StartsWith('+') && phoneNumber.Length > 1)
            {
                // Проверка, что после "+" идут только цифры
                return phoneNumber.Substring(1).All(char.IsDigit);
            }

            return false;
        }

        private bool IsValidQuestion(string question)
        {
            return !string.IsNullOrWhiteSpace(question) && question != "Напишите ваш вопрос или выберите из выпадающего списка";
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (countryComboBox.SelectedItem == null)
            {
                // Если ничего не выбрано, устанавливается минимум 6 символов
                textBoxNumber.MaxLength = 15; // Максимум 15 символов для международных номеров
                if (textBoxNumber.Text.Length < 6)
                {
                    MessageBox.Show("Номер должен содержать минимум 6 символов.");
                }
                return; // Выход из метода, если выбранный элемент равен null
            }

            if (countryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Россия")
                {
                    // Устанавливаем начальное значение для России
                    if (!textBoxNumber.Text.StartsWith("+7"))
                    {
                        textBoxNumber.Text = "+7";
                    }

                    textBoxNumber.MaxLength = 12; // Минимум 12 символов (включая +7)

                    // Проверка длины номера
                    if (textBoxNumber.Text.Length < 12)
                    {
                        MessageBox.Show("Российский номер должен содержать минимум 12 символов (включая +7).");
                    }
                }
                else if (selectedItem.Content.ToString() == "Другая страна")
                {
                    // Очищаем текстовое поле для другой страны
                    textBoxNumber.Text = "+";
                    textBoxNumber.MaxLength = 15; // Максимум 15 символов для международных номеров

                    // Проверка длины номера
                    if (textBoxNumber.Text.Length < 6)
                    {
                        MessageBox.Show("Номер из другой страны должен содержать минимум 6 символов.");
                    }
                }
            }
        }

        private void readyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (readyComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                textBoxQuestion.Text = selectedItem.Content.ToString();
            }
        }
    }   
    
}
