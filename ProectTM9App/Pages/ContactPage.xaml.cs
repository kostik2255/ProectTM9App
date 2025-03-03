using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для ContactPage.xaml
    /// </summary>
    public partial class ContactPage : Page
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void LocationTB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var location = "142805 Московская обл., г. Ступино, Приокский пер., 7";
            Clipboard.SetText(location);
            MessageBox.Show("Адрес скопирован");
        }

        private void EmailTB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var email = "info@proekt-tm9.ru";
            Clipboard.SetText(email);
            MessageBox.Show("Email скопирован");
        }

        private void PhoneTB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var phoneNumber = "+7 (496) 641-57-70";
            Clipboard.SetText(phoneNumber);
            MessageBox.Show("Номер телефона скопирован");
        }
    }
}
