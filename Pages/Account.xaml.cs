using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Ecycle.Pages
{
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
        }

        // Placeholder for handling Save New Profile button click event
        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Profile saved successfully!", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for Product List button click event
        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            // Navigasi ke halaman UserProduct.xaml
            NavigationService?.Navigate(new UserProduct());
        }
    }
}
