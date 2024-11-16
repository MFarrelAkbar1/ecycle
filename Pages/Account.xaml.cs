using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Ecycle.Models; // Import UserSession

namespace Ecycle.Pages
{
    public partial class Account : Page
    {
        private readonly string updateProfileEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/update";

        public Account()
        {
            InitializeComponent();

            // Tampilkan username yang telah login
            txtUserName.Text = UserSession.Username ?? "Guest"; // Default jika username kosong
        }

        // Event handler for TextBox GotFocus
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            TextBlock placeholder = FindPlaceholderForControl(textBox);

            if (placeholder != null)
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
        }

        // Event handler for TextBox LostFocus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            TextBlock placeholder = FindPlaceholderForControl(textBox);

            if (placeholder != null && string.IsNullOrEmpty(textBox.Text))
            {
                placeholder.Visibility = Visibility.Visible;
            }
        }

        // Event handler for PasswordBox GotFocus
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            TextBlock placeholder = FindPlaceholderForControl(passwordBox);

            if (placeholder != null)
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
        }

        // Event handler for PasswordBox LostFocus
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            TextBlock placeholder = FindPlaceholderForControl(passwordBox);

            if (placeholder != null && string.IsNullOrEmpty(passwordBox.Password))
            {
                placeholder.Visibility = Visibility.Visible;
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
            string currentUsername_ = txtCurrentUsername.Text;
            string currentPassword_ = txtCurrentPassword.Password;
            string newUsername_ = txtNewUsername.Text;
            string newPassword_ = txtNewPassword.Password;
            string newAlamat_ = txtNewAlamat.Text;
            string newTelepon_ = txtNewTelepon.Text;

            if (string.IsNullOrWhiteSpace(currentUsername_) || string.IsNullOrWhiteSpace(currentPassword_) ||
                string.IsNullOrWhiteSpace(newUsername_) || string.IsNullOrWhiteSpace(newPassword_))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isUpdated = await UpdateUserProfileAsync(currentUsername_, currentPassword_, newUsername_, newPassword_, newAlamat_, newTelepon_);
            if (isUpdated)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> UpdateUserProfileAsync(string currentUsername_, string currentPassword_, string newUsername_, string newPassword_, string newAlamat_, string newTelepon_)
        {
            try
            {
                using var httpClient = new HttpClient();
                var userProfileUpdate = new
                {
                    nama = currentUsername_,
                    newUsername = newUsername_,
                    password = currentPassword_,
                    newPassword = newPassword_,
                    alamat = newAlamat_,
                    telepon = newTelepon_
                };

                string json = JsonConvert.SerializeObject(userProfileUpdate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PatchAsync(updateProfileEndpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new UserProduct());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Hapus sesi pengguna
            UserSession.Username = null;
            UserSession.Password = null;

            // Tampilkan pesan logout
            MessageBox.Show("You have been logged out.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigasi ke halaman login
            Login loginWindow = new Login();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
