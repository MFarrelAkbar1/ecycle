﻿using System;
using System.Windows;
using System.Windows.Input;
using Npgsql;
using System.Threading.Tasks;

namespace Ecycle
{
    public partial class Signup : Window
    {
        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;

        // Replace these with your actual Azure PostgreSQL credentials
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

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

        // Placeholder management and visibility toggle methods remain unchanged...

        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string password = txtPasswordBox.Password;
            string confirmPassword = txtConfirmPasswordBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Save to database
            bool isRegistered = await RegisterUserAsync(fullName, email, password);
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

        private async Task<bool> RegisterUserAsync(string fullName, string email, string password)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();

                string query = "INSERT INTO users (full_name, email, password) VALUES (@fullName, @email, @password)";
                using var cmd = new NpgsqlCommand(query, conn);

                // Using parameterized queries to prevent SQL injection
                cmd.Parameters.AddWithValue("fullName", fullName);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("password", password); // For security, hash the password before storing

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0; // True if insertion was successful
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
