using ProectTM9App.Classes;
using ProectTM9App.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProectTM9App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());

            // Создаем таймер с интервалом 1 секунда
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick; 

            // Запускаем таймер при загрузке окна
            Loaded += (sender, e) => timer.Start();

            // Останавливаем таймер при закрытии окна
            Closing += (sender, e) => timer.Stop();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            labelDateTime.Content = Date.GetCurrentTime();
        }

        private void MainPageBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        private void TypesOfWorkBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TypesOfWorkPage());
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AboutPage());
        }

        private void TeamBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeamPage());
        }

        private void FeedbackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FeedbackPage());
        }

        private void ContactBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ContactPage());
        }

    }
}