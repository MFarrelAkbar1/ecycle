using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ecycle.Models;
using Npgsql;

namespace Ecycle.Pages
{
    public partial class Home : Page
    {
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

        public Home()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        // Metode untuk mengambil data produk dari database Azure
        private async Task<List<ProductModel>> GetProductsAsync()
        {
            var products = new List<ProductModel>();
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();

                string query = "SELECT product_id, name, description, price, stock, sold, seller_name, seller_location FROM products";
                using var cmd = new NpgsqlCommand(query, conn);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    products.Add(new ProductModel
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        Stock = reader.GetInt32(4),
                        Sold = reader.GetInt32(5),
                        SellerName = reader.IsDBNull(6) ? null : reader.GetString(6),
                        SellerLocation = reader.IsDBNull(7) ? null : reader.GetString(7)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return products;
        }

        // Menampilkan produk di UI
        private async Task LoadProductsAsync()
        {
            var products = await GetProductsAsync();
            ProductWrapPanel.Children.Clear();

            foreach (var product in products)
            {
                // Membuat Button untuk produk
                var button = new Button
                {
                    Style = (Style)FindResource("TransparentButtonStyle"),
                    Margin = new Thickness(10)
                };
                button.Click += (s, e) => ProductCard_Click(product);

                // Border untuk menampung detail produk
                var border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Background = Brushes.White
                };

                var stackPanel = new StackPanel();

                // Menampilkan nama dan harga produk
                stackPanel.Children.Add(new TextBlock { Text = product.Name, FontWeight = FontWeights.Bold, FontSize = 14, Foreground = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new TextBlock { Text = $"{product.Price:C}", FontWeight = FontWeights.Bold, FontSize = 12, Foreground = Brushes.Green, HorizontalAlignment = HorizontalAlignment.Center });

                border.Child = stackPanel;
                button.Content = border;
                ProductWrapPanel.Children.Add(button);
            }
        }

        // Menangani navigasi ke halaman Product dengan mengirim data produk
        private void ProductCard_Click(ProductModel product)
        {
            var productPage = new Product();
            productPage.LoadProductDetails(product);
            NavigationService.Navigate(productPage);
        }
    }
}
