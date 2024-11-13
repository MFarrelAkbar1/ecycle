using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ecycle.Models; // Ensure this namespace is imported
using Newtonsoft.Json;

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        private readonly string createProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/post";

        public UserProduct()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // Navigate back to the previous page
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductForm.Visibility = Visibility.Visible;
            ResetForm(); // Clear existing form fields for a new product
        }

        private void ResetForm()
        {
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductStock.Text = string.Empty;
        }

        // Event handler for the TextBox GotFocus event (removes placeholder text)
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Your existing logic goes here
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Your existing logic goes here
        }

        private void LoadProducts()
        {
            // Logic to load products goes here
        }

        private async void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // Validate fields
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductDescription.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtProductStock.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newProduct = new
            {
                nama = txtProductName.Text,
                deskripsi = txtProductDescription.Text,
                stok = int.Parse(txtProductStock.Text),  // Ensure this is correct
                harga = int.Parse(txtProductPrice.Text),
                ongkosKirim = 0, // You can also add this field if needed
                kategoriID = 0, // Adjust this as necessary
                penjualID = 1, // Replace with the actual user's ID if applicable
                bahanID = 0 // Adjust as necessary
            };

            bool isSuccessful = await CreateProductAsync(newProduct);
            if (isSuccessful)
            {
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadProducts(); // Refresh the product list after saving
                ProductForm.Visibility = Visibility.Collapsed; // Hide form after successful submission
            }
            else
            {
                MessageBox.Show("Failed to create product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> CreateProductAsync(object product)
        {
            try
            {
                using var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(createProductEndpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
