using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ecycle.Models; // Ensure this namespace is imported
using Npgsql;

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

        public UserProduct()
        {
            InitializeComponent();
            LoadProducts();
        }

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

        public async void LoadProducts()
        {
            var products = await GetProductsAsync();
            ProductList.Children.Clear();

            foreach (var product in products)
            {
                var border = new Border
                {
                    Background = System.Windows.Media.Brushes.LightGray,
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5)
                };

                var stackPanel = new StackPanel();
                stackPanel.Children.Add(new TextBlock { Text = product.Name, FontWeight = FontWeights.Bold });
                stackPanel.Children.Add(new TextBlock { Text = $"Price: {product.Price:C}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Stock: {product.Stock}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Sold: {product.Sold}" });
                stackPanel.Children.Add(new TextBlock { Text = product.Description, TextWrapping = TextWrapping.Wrap });

                border.Child = stackPanel;
                ProductList.Children.Add(border);
            }
        }

        // Event handler for the Back button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // Navigate back to the previous page
        }

        // Event handler for Add Product button
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Show product form for adding a new product
            ProductForm.Visibility = Visibility.Visible;
            ResetForm(); // Clear existing form fields for a new product
        }

        private async void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // Logic for saving a new or updated product
        }

        private async void DeleteProduct(int productId)
        {
            // Logic for deleting a product
        }

        private void ResetForm()
        {
            // Clear all form fields
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductStock.Text = string.Empty;
        }
    }
}
