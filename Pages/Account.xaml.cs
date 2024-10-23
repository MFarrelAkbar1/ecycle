using System;
using System.Windows;
using System.Windows.Controls;

namespace Ecycle.Pages
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
        }

        // Placeholder for handling Save New Profile button click event
        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            // Add code to save the profile details
            MessageBox.Show("Profile saved successfully!", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
