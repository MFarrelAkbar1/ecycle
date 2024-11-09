using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ecycle.Models;
using Newtonsoft.Json;

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // Navigate back to the previous page
        }

        // Load product details from Azure backend using product ID
        public async void LoadProductDetails(int productId)
        {
            try
            {
                string url = $"{ApiBaseUrl}{productId}";
                var response = await _httpClient.GetStringAsync(url);

                var product = JsonConvert.DeserializeObject<ProductModel>(response);
                if (product != null)
                {
                    DisplayProductDetails(product);
                }
                else
                {
                    MessageBox.Show("Product not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Display the fetched product details in the UI
        private void DisplayProductDetails(ProductModel product)
        {
            txtProductName.Text = product.Name;
            txtProductSold.Text = $"Sold: {product.Sold}";
            txtProductStock.Text = $"Available stock: {product.Stock}";
            txtProductPrice.Text = $"{product.Price:C}";  // Format price as currency
            txtProductDescription.Text = product.Description;
            txtProductShipping.Text = $"Shipping cost: {product.ShippingCost:C}";  // Display shipping cost
            txtProductSeller.Text = product.SellerName ?? "Unknown Seller";
            txtSellerLocation.Text = product.SellerLocation ?? "Location not specified";
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Product added to cart!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BuyNow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Proceeding to checkout...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
