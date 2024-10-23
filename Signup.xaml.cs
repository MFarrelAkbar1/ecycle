using System;
using System.Windows;
using System.Windows.Controls;

namespace Ecycle
{
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Logic for email change (optional)
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Logic for password change (optional)
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            // Example sign-up logic
            MessageBox.Show("Registration successful!");
            // Close the signup window or redirect to another page
            this.Close();
        }
    }
}
