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
            
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Registration successful!");
            this.Close();
        }
    }
}
