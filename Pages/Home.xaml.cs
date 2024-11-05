using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Ecycle.Pages
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ProductCard_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Product.xaml page
            NavigationService.Navigate(new Product());
        }
    }
}
