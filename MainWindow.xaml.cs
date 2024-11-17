using System.Windows;
using System.Windows.Controls;

namespace Ecycle
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Membuka jendela dalam mode fullscreen
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None; // Opsi untuk menghilangkan border
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

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void MainFrame_Navigated_1(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
