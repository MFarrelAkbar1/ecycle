using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ecycle.Models;

namespace Ecycle.Pages
{
    public partial class Account : Page
    {
        private readonly string updateProfileEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/update";
        private readonly HttpClient httpClient;

        public Account()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Display logged in username
            txtUserName.Text = UserSession.Username ?? "Guest";

            // Pre-fill current username if available
            if (!string.IsNullOrEmpty(UserSession.Username))
            {
                txtCurrentUsername.Text = UserSession.Username;
                txtCurrentUsernamePlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        // Event handler for TextBox GotFocus
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var placeholder = FindPlaceholderForControl(textBox);
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Collapsed;
                }
            }
        }

        // Event handler for TextBox LostFocus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var placeholder = FindPlaceholderForControl(textBox);
                if (placeholder != null && string.IsNullOrEmpty(textBox.Text))
                {
                    placeholder.Visibility = Visibility.Visible;
                }
            }
        }

        // Event handler for PasswordBox GotFocus
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var placeholder = FindPlaceholderForControl(passwordBox);
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Collapsed;
                }
            }
        }

        // Event handler for PasswordBox LostFocus
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var placeholder = FindPlaceholderForControl(passwordBox);
                if (placeholder != null && string.IsNullOrEmpty(passwordBox.Password))
                {
                    placeholder.Visibility = Visibility.Visible;
                }
            }
        }

        // Helper method to find the placeholder TextBlock for a given control
        private TextBlock FindPlaceholderForControl(Control control)
        {
            string placeholderName = control.Name + "Placeholder";
            return this.FindName(placeholderName) as TextBlock;
        }

        private async void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Trim all input values
                string currentUsername = txtCurrentUsername.Text.Trim();
                string currentPassword = txtCurrentPassword.Password.Trim();
                string newUsername = txtNewUsername.Text.Trim();
                string newPassword = txtNewPassword.Password.Trim();
                string newAlamat = txtNewAlamat.Text.Trim();
                string newTelepon = txtNewTelepon.Text.Trim();

                // Validate required fields
                if (string.IsNullOrWhiteSpace(currentUsername))
                {
                    MessageBox.Show("Current username is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(currentPassword))
                {
                    MessageBox.Show("Current password is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(newUsername))
                {
                    MessageBox.Show("New username is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("New password is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var updateResult = await UpdateUserProfileAsync(currentUsername, currentPassword, newUsername, newPassword, newAlamat, newTelepon);

                if (updateResult.success)
                {
                    // Update session with new username
                    UserSession.Username = newUsername;
                    txtUserName.Text = newUsername;

                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(updateResult.message ?? "Failed to update profile. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<(bool success, string? message)> UpdateUserProfileAsync(
            string currentUsername, string currentPassword,
            string newUsername, string newPassword,
            string newAlamat, string newTelepon)
        {
            try
            {
                var userProfileUpdate = new
                {
                    nama = currentUsername,
                    newUsername = newUsername,
                    password = currentPassword,
                    newPassword = newPassword,
                    alamat = newAlamat,
                    telepon = newTelepon
                };

                string json = JsonConvert.SerializeObject(userProfileUpdate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync(updateProfileEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    try
                    {
                        dynamic? error = JsonConvert.DeserializeObject(responseContent);
                        return (false, error?.message?.ToString() ?? "Unknown error occurred");
                    }
                    catch
                    {
                        return (false, $"Server returned status code: {response.StatusCode}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return (false, $"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Unexpected error: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            // Clear all text boxes
            txtCurrentUsername.Text = "";
            txtCurrentPassword.Password = "";
            txtNewUsername.Text = "";
            txtNewPassword.Password = "";
            txtNewAlamat.Text = "";
            txtNewTelepon.Text = "";

            // Show all placeholders
            txtCurrentUsernamePlaceholder.Visibility = Visibility.Visible;
            txtCurrentPasswordPlaceholder.Visibility = Visibility.Visible;
            txtNewUsernamePlaceholder.Visibility = Visibility.Visible;
            txtNewPasswordPlaceholder.Visibility = Visibility.Visible;
            txtNewAlamatPlaceholder.Visibility = Visibility.Visible;
            txtNewTeleponPlaceholder.Visibility = Visibility.Visible;
        }

        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new UserProduct());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Clear user session
            UserSession.Username = null;
            UserSession.Password = null;

            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigate to login window
            Login loginWindow = new Login();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}