using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Npgsql;
using System.Threading.Tasks;

namespace Ecycle
{
    public partial class Login : Window
    {
        private bool isPasswordVisible = false;
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

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

        private async Task<bool> VerifyCredentialsAsync(string username, string password)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();

                string query = "SELECT COUNT(1) FROM users WHERE username = @username AND password = @password";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password); // Change this to a hash if passwords are stored hashed

                int result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return result > 0; // Returns true if a matching record is found
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to database: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
