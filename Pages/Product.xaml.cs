using System.Windows;
using System.Windows.Controls;

namespace Ecycle.Pages
{
    public partial class Product : Page
    {
        public Product()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
