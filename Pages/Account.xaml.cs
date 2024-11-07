using System;
using System.Windows;
using Npgsql;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ecycle.Pages
{
    public partial class Account : Page
    {
        // Replace with actual Azure PostgreSQL credentials
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

        public Account()
        {
            InitializeComponent();
        }

        // Placeholder for handling Save New Profile button click event
        private async void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = txtFullname.Text;
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Username and Password fields cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isUpdated = await UpdateUserProfileAsync(newUsername, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Profile saved successfully!", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> UpdateUserProfileAsync(string newUsername, string newPassword)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();

                string query = "UPDATE users SET username = @newUsername, password = @newPassword WHERE user_id = @userId";

                using var cmd = new NpgsqlCommand(query, conn);

                // Using parameterized queries to prevent SQL injection
                cmd.Parameters.AddWithValue("newUsername", newUsername);
                cmd.Parameters.AddWithValue("newPassword", newPassword); // Consider hashing the password for security
                cmd.Parameters.AddWithValue("userId", GetUserId()); // Replace with a method or variable to fetch the current user's ID

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0; // True if update was successful
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Placeholder method to fetch the user ID for the currently logged-in user
        private int GetUserId()
        {
            // Return the current user's ID
            return 1; // Replace this with actual logic to retrieve the logged-in user's ID
        }

        // Event handler for Product List button click event
        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the UserProduct page
            NavigationService?.Navigate(new UserProduct());
        }
    }
}
