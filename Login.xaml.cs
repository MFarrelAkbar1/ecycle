﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ecycle.Models; // Tambahkan namespace untuk UserSession

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
                // Simpan username dan password ke UserSession setelah login berhasil
                UserSession.Username = enteredUsername;
                UserSession.Password = enteredPassword;

                // Navigasi ke halaman utama
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> VerifyCredentialsAsync(string username, string password_)
        {
            try
            {
                using HttpClient client = new HttpClient();

                var body = new
                {
                    nama = username,
                    password = password_
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync(authEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Login failed: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Parse the API response
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                // Store user information in UserSession
                UserSession.PenggunaID = loginResponse.penggunaID;
                UserSession.Username = loginResponse.nama;
                UserSession.Password = loginResponse.password;
                UserSession.Alamat = loginResponse.alamat;
                UserSession.Telepon = loginResponse.telepon;
                UserSession.Token = loginResponse.token;

                // Show the full JSON response in a MessageBox (for debugging)
                MessageBox.Show($"Login Successful!\n\nResponse: {UserSession.PenggunaID}", "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
