using System;
using System.Windows;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ecycle
{
    public partial class Signup : Window
    {
        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;

        private readonly string registerEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/register";

        public Signup()
        {
            InitializeComponent();
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender == txtFullName)
                txtFullNamePlaceholder.Visibility = Visibility.Collapsed;
            else if (sender == txtEmail)
                txtEmailPlaceholder.Visibility = Visibility.Collapsed;
            else if (sender == txtAddress)
                txtAddressPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == txtFullName && string.IsNullOrWhiteSpace(txtFullName.Text))
                txtFullNamePlaceholder.Visibility = Visibility.Visible;
            else if (sender == txtEmail && string.IsNullOrWhiteSpace(txtEmail.Text))
                txtEmailPlaceholder.Visibility = Visibility.Visible;
            else if (sender == txtAddress && string.IsNullOrWhiteSpace(txtAddress.Text))
                txtAddressPlaceholder.Visibility = Visibility.Visible;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender == txtPasswordBox)
                txtPasswordPlaceholder.Visibility = Visibility.Collapsed;
            else if (sender == txtConfirmPasswordBox)
                txtConfirmPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == txtPasswordBox && string.IsNullOrWhiteSpace(txtPasswordBox.Password))
                txtPasswordPlaceholder.Visibility = Visibility.Visible;
            else if (sender == txtConfirmPasswordBox && string.IsNullOrWhiteSpace(txtConfirmPasswordBox.Password))
                txtConfirmPasswordPlaceholder.Visibility = Visibility.Visible;
        }

        private void PasswordEyeIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isPasswordVisible)
            {
                txtPasswordText.Visibility = Visibility.Collapsed;
                txtPasswordBox.Visibility = Visibility.Visible;
                txtPasswordBox.Password = txtPasswordText.Text;
                isPasswordVisible = false;
            }
            else
            {
                txtPasswordText.Text = txtPasswordBox.Password;
                txtPasswordText.Visibility = Visibility.Visible;
                txtPasswordBox.Visibility = Visibility.Collapsed;
                isPasswordVisible = true;
            }
        }

        private void ConfirmPasswordEyeIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isConfirmPasswordVisible)
            {
                txtConfirmPasswordText.Visibility = Visibility.Collapsed;
                txtConfirmPasswordBox.Visibility = Visibility.Visible;
                txtConfirmPasswordBox.Password = txtConfirmPasswordText.Text;
                isConfirmPasswordVisible = false;
            }
            else
            {
                txtConfirmPasswordText.Text = txtConfirmPasswordBox.Password;
                txtConfirmPasswordText.Visibility = Visibility.Visible;
                txtConfirmPasswordBox.Visibility = Visibility.Collapsed;
                isConfirmPasswordVisible = true;
            }
        }

        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string password = txtPasswordBox.Password;
            string confirmPassword = txtConfirmPasswordBox.Password;
            string address = txtAddress.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isRegistered = await RegisterUserAsync(fullName, password, address);
            if (isRegistered)
            {
                MessageBox.Show($"Welcome, {fullName}! You have registered successfully.", "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                BackToLogin_Click(sender, e); // Redirect to login
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> RegisterUserAsync(string nama, string password, string alamat)
        {
            try
            {
                using var httpClient = new HttpClient();

                var user = new
                {
                    nama,
                    password,
                    alamat,
                    telepon = "123" // Dummy data for 'telepon'
                };

                string json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(registerEndpoint, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to registration server: {ex.Message}", "Network Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
