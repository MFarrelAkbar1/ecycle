using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ecycle.Models;
using Newtonsoft.Json;

namespace Ecycle.Pages
{
    public partial class Home : Page
    {
        private static readonly HttpClient client = new HttpClient();

        public Home()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        // Method to fetch product data from the Azure endpoint
        private async Task<List<HomeModel>> GetProductsAsync()
        {
            var products = new List<HomeModel>();

            try
            {
                var response = await client.GetAsync("https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response into a list of ProductModel objects
                products = JsonConvert.DeserializeObject<List<HomeModel>>(responseBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return products;
        }

        // Method to display product data dynamically in the UI
        private async Task LoadProductsAsync()
        {
            var products = await GetProductsAsync();
            ProductWrapPanel.Children.Clear();

            foreach (var product in products)
            {
                // Create a Button for each product
                var button = new Button
                {
                    Style = (Style)FindResource("TransparentButtonStyle"),
                    Margin = new Thickness(10)
                };
                button.Click += (s, e) => ProductCard_Click(product);

                // Create a Border to hold product details
                var border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Background = Brushes.White
                };

                var stackPanel = new StackPanel();

                // Placeholder for Image (Gray Color Block)
                var imagePlaceholder = new Border
                {
                    Background = Brushes.Gray,
                    Width = 100,
                    Height = 100,
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 0, 10)
                };
                stackPanel.Children.Add(imagePlaceholder);

                // Add product name and price if available
                if (!string.IsNullOrEmpty(product.Nama))
                {
                    stackPanel.Children.Add(new TextBlock
                    {
                        Text = product.Nama,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14,
                        Foreground = Brushes.Black,
                        HorizontalAlignment = HorizontalAlignment.Center
                    });
                }

                if (!string.IsNullOrEmpty(product.Harga))
                {
                    stackPanel.Children.Add(new TextBlock
                    {
                        Text = $"{product.Harga}",
                        FontWeight = FontWeights.Bold,
                        FontSize = 12,
                        Foreground = Brushes.Green,
                        HorizontalAlignment = HorizontalAlignment.Center
                    });
                }

                border.Child = stackPanel;
                button.Content = border;
                ProductWrapPanel.Children.Add(button);  
            }
        }

        // Navigate to Product page with selected product data
        private void ProductCard_Click(HomeModel product)
        {
            if (int.TryParse(product.ProdukID, out int productId))
            {
                var productPage = new Product();
                productPage.LoadProductDetails(productId); // Pass converted product ID
                NavigationService.Navigate(productPage);
            }
            else
            {
                MessageBox.Show("Invalid Product ID format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
