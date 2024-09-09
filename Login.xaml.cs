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
using System.Windows.Shapes;
using ecycle;

namespace ecycle
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            string validUsername = "admin";
            string validPassword = "password123";

            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Password;

            if (enteredUsername == validUsername && enteredPassword == validPassword)
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {

                MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Fitur Sign Up belum diimplementasikan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void textUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
                textUsername.Visibility = Visibility.Hidden;
            else
                textUsername.Visibility = Visibility.Visible;
        }

        private void textUsername_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void textPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password))
                textPassword.Visibility = Visibility.Hidden;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
