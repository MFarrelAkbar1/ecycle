using System;
using System.Net.Http;
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

        public async void LoadProductDetails(int productId)
        {
            try
            {
                string url = $"{ApiBaseUrl}{productId}";
                var response = await _httpClient.GetStringAsync(url);

                // Deserialize JSON response into ProductModel
                var product = JsonConvert.DeserializeObject<ProductModel>(response);

                if (product != null)
                {
                    // Assign data to UI elements
                    txtProductId.Text = product.ProdukID.ToString();
                    txtProductName.Text = product.Name ?? "No Name";
                    txtProductPrice.Text = $"Price: Rp{product.Price:N}";
                    txtProductDescription.Text = product.Description ?? "No Description";
                    txtProductStock.Text = $"Stock: {product.Stock}";
                    txtProductSold.Text = $"Sold: {product.Sold}";
                    txtProductShipping.Text = $"Shipping Cost: Rp{product.ShippingCost:N}";
                }
                else
                {
                    MessageBox.Show("Product not found or response is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            try
            {
                int quantity = int.Parse(txtQuantity.Text);
                string priceText = txtProductPrice.Text.Replace("Price: Rp", "").Replace(",", "").Trim();
                decimal unitPrice = decimal.Parse(priceText);

                CartStorage.Items.Add(new CartItemModel
                {
                    ProductId = int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });

                MessageBox.Show("Product added to cart!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Navigate to the Cart page after adding to cart
                NavigationService.Navigate(new Cart());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add product to cart: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
