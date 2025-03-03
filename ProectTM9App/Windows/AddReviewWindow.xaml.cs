using ProectTM9App.Classes;
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
using System.Windows.Shapes;

namespace ProectTM9App.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddReviewWindow.xaml
    /// </summary>
    public partial class AddReviewWindow : Window
    {
        private ReviewViewModel _viewModel;

        public AddReviewWindow(ReviewViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void AddReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            var review = new Review
            {
                FullName = NameTextBox.Text,
                Company = CompanyTextBox.Text,
                Position = PositionTextBox.Text,
                Feedback = ReviewTextBox.Text
            };

            _viewModel.Reviews.Add(review);
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.SaveReviews();
        }
    }
}
