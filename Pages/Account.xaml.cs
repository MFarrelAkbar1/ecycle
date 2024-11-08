using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Ecycle.Pages
{
    public partial class Account : Page
    {
        private readonly string updateProfileEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/auth/update";

        public Account()
        {
            InitializeComponent(); // This is auto-generated; do not add a second InitializeComponent() here
        }

        private async void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            string currentUsername = txtCurrentUsername.Text;
            string currentPassword = txtCurrentPassword.Password;
            string newUsername = txtNewUsername.Text;
            string newPassword = txtNewPassword.Password;

            if (string.IsNullOrWhiteSpace(currentUsername) || string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isUpdated = await UpdateUserProfileAsync(currentUsername, currentPassword, newUsername, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> UpdateUserProfileAsync(string currentUsername, string currentPassword, string newUsername, string newPassword)
        {
            try
            {
                using var httpClient = new HttpClient();
                var userProfileUpdate = new
                {
                    nama = currentUsername,
                    newNama = newUsername,
                    password = currentPassword,
                    newPassword = newPassword
                };

                string json = JsonConvert.SerializeObject(userProfileUpdate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(updateProfileEndpoint, content);
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
    }
}
