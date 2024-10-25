using System.Windows;
using System.Windows.Controls;

namespace Ecycle
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as NavButton;
            if (selected != null && navframe != null)
            {
                navframe.Navigate(selected.Navlink);
                welcomePanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}