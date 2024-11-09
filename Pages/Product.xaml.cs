using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Ecycle.Models;
using Newtonsoft.Json.Linq;

namespace Ecycle.Pages
{
    public partial class Product : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/";

        public Product()
        {
            InitializeComponent();
        }

        public async void LoadProductDetails(int productId)
        {
            try
            {
                string url = $"{ApiBaseUrl}{productId}";
                var response = await _httpClient.GetStringAsync(url);

                var productData = JObject.Parse(response);

                string produkID = productData["produkID"]?.ToString();
                string nama = productData["nama"]?.ToString();
                string harga = productData["harga"]?.ToString();

                txtProductName.Text = nama ?? "No Name";
                txtProductPrice.Text = $"Price: {harga ?? "0"}";
                txtProductId.Text = produkID; // Assuming txtProductId is a hidden TextBlock for product ID
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);
            txtQuantity.Text = (++quantity).ToString();
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);
            if (quantity > 1)
            {
                txtQuantity.Text = (--quantity).ToString();
            }
            else
            {
                MessageBox.Show("Minimum quantity is 1.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);

            CartStorage.Items.Add(new CartItemModel
            {
                ProductId = int.Parse(txtProductId.Text), // Assuming txtProductId is the hidden product ID field
                ProductName = txtProductName.Text,
                Quantity = quantity,
                UnitPrice = decimal.Parse(txtProductPrice.Text.Replace("Price: ", ""))
            });

            MessageBox.Show("Product added to cart!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
