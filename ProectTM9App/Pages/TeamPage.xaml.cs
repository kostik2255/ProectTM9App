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
    /// Логика взаимодействия для TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        public TeamPage()
        {
            InitializeComponent();
        }

        // 1
        private void ShowHiddenGrid(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid");
            hiddenGrid.Visibility = Visibility.Visible;
        }

        private void HideHiddenGrid(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid");
            hiddenGrid.Visibility = Visibility.Collapsed;
        }

        // 2
        private void ShowHiddenGrid1(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid1");
            hiddenGrid.Visibility = Visibility.Visible;
        }

        private void HideHiddenGrid1(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid1");
            hiddenGrid.Visibility = Visibility.Collapsed;
        }

        // 3
        private void ShowHiddenGrid2(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid2");
            hiddenGrid.Visibility = Visibility.Visible;
        }

        private void HideHiddenGrid2(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid2");
            hiddenGrid.Visibility = Visibility.Collapsed;
        }

        // 4
        private void ShowHiddenGrid3(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid3");
            hiddenGrid.Visibility = Visibility.Visible;
        }

        private void HideHiddenGrid3(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            Grid hiddenGrid = (Grid)grid.FindName("hiddenGrid3");
            hiddenGrid.Visibility = Visibility.Collapsed;
        }
    }
}
