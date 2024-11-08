using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ecycle
{
    public partial class Login : Window
    {
        private bool isPasswordVisible = false;
        private readonly string authEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/login";

        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsernamePlaceholder.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsernamePlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPasswordBox.Password))
            {
                txtPasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void EyeIcon_MouseDown(object sender, MouseButtonEventArgs e)
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

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = isPasswordVisible ? txtPasswordText.Text : txtPasswordBox.Password;

            bool isAuthenticated = await VerifyCredentialsAsync(enteredUsername, enteredPassword);

            if (isAuthenticated)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> VerifyCredentialsAsync(string username, string thispassword)
        {
            try
            {
                HttpClient client = new HttpClient();

                var body = new
                {
                    nama = username,
                    password = thispassword
                };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync("https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/login", content);
                MessageBox.Show(response.StatusCode.ToString());
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to authentication server: {ex.Message}", "Network Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Signup signupWindow = new Signup();
            signupWindow.Show();
            this.Close();
        }
    }
}
