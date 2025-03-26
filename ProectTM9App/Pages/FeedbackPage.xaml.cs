using ProectTM9App.Classes;
using ProectTM9App.Windows;
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
    /// Логика взаимодействия для FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        private ReviewViewModel _viewModel;
        public FeedbackPage()
        {
            InitializeComponent();
            _viewModel = new ReviewViewModel();
            DataContext = _viewModel;

            // Подписка на изменения в коллекции
            _viewModel.Reviews.CollectionChanged += (s, e) => UpdateReviewsDisplay();

        }

        private void LeaveReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            var addReviewWindow = new AddReviewWindow();
            addReviewWindow.ShowDialog();
        }

        private void UpdateReviewsDisplay()
        {
            ReviewsStackPanel.Children.Clear();

            foreach (var review in _viewModel.Reviews)
            {
                var reviewBorder = new Border
                {
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(5),
                    Padding = new Thickness(10)
                };

                var reviewContent = new StackPanel();

                reviewContent.Children.Add(new TextBlock { Text = review.FullName, FontWeight = FontWeights.Bold });
                reviewContent.Children.Add(new TextBlock { Text = review.Company });
                reviewContent.Children.Add(new TextBlock { Text = review.Position });
                reviewContent.Children.Add(new TextBlock { Text = review.Feedback });

                reviewBorder.Child = reviewContent;
                ReviewsStackPanel.Children.Add(reviewBorder);
            }
        }

        private void LoadReviewsBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadReviews();
            UpdateReviewsDisplay();

            LoadReviewsBtn.Visibility = Visibility.Collapsed;
        }
    }
}
