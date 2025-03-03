using ProectTM9Api.Models;
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
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetConsultationBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = textBoxName.Text;
            var email = textBoxEmail.Text;
            var phoneNumber = textBoxNumber.Text;

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
                MessageBox.Show("Введите корректный номер телефона.");
                return;
            }

            var request = new ConsultationRequest
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            using (var httpClient = new HttpClient())
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7157/api/Consultation", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Запрос успешно отправлен.");
                }
                else
                {
                    MessageBox.Show("Ошибка при отправке запроса.");
                }
            }
        }
        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name != "Ваше имя";
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains('@');
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Проверка на наличие символа "+" в начале и на наличие "7" сразу после
            if (phoneNumber.StartsWith("+7") && phoneNumber.Length == 12)
            {
                // Проверяем, что после "+7" идут только цифры
                return phoneNumber.Substring(2).All(char.IsDigit);
            }
            
            return false;
        }



        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбранный элемент не равен null
            if (countryComboBox.SelectedItem == null)
            {
                return; // Выходим из метода, если выбранный элемент равен null
            }

            if (countryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Россия")
                {
                    // Устанавливаем начальное значение для России
                    if (!textBoxNumber.Text.StartsWith("+7"))
                    {
                        textBoxNumber.Text = "+7"; // Устанавливаем "+7", если это не так
                    }

                    // Проверяем, что введено ровно 10 цифр после "+7"
                    var numberPart = textBoxNumber.Text.Substring(2);
                    if (!Regex.IsMatch(numberPart, @"^d{0,10}$")) // Исправлено: добавлены символы для обозначения цифр
                    {
                        // Если введено больше 10 цифр, обрезаем до 10
                        textBoxNumber.Text = "+7" + numberPart.Substring(0, Math.Min(numberPart.Length, 10));
                    }
                    textBoxNumber.MaxLength = 12; // Максимум 12 символов (включая +7)
                }
                else if (selectedItem.Content.ToString() == "Другая страна")
                {
                    // Очищаем текстовое поле для другой страны
                    textBoxNumber.Text = "";
                    textBoxNumber.MaxLength = 13; // Максимум 13 символов
                }
            }
        }

    }   
    
}
