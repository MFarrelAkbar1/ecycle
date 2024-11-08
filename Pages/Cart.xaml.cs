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
    public partial class Cart : Page
    {
        // Gantilah string koneksi ini dengan informasi koneksi Azure PostgreSQL Anda.
        private readonly string connectionString = "Host=<your_azure_host>;Port=5432;Username=<your_username>;Password=<your_password>;Database=<your_database>";

        public Cart()
        {
            InitializeComponent();
            Loaded += Page_Loaded; // Memanggil metode LoadCartItems saat halaman dimuat
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCartItems();
        }

        // Metode untuk mengambil item cart dari database Azure
        private async Task<List<CartItemModel>> GetCartItemsAsync(int userId)
        {
            var cartItems = new List<CartItemModel>();
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();

                // Query untuk mengambil item cart berdasarkan user ID
                string query = "SELECT cart_item_id, product_id, product_name, quantity, unit_price FROM cart_items WHERE user_id = @userId";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("userId", userId);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    cartItems.Add(new CartItemModel
                    {
                        CartItemId = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        ProductName = reader.GetString(2),
                        Quantity = reader.GetInt32(3),
                        UnitPrice = reader.GetDecimal(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cart items: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return cartItems;
        }

        // Memuat item cart ke dalam StackPanel di Cart.xaml
        private async Task LoadCartItems()
        {
            int userId = 1; // Gantilah dengan ID pengguna yang sesuai
            var cartItems = await GetCartItemsAsync(userId);

            CartItemsPanel.Children.Clear(); // Mengosongkan konten sebelumnya

            foreach (var item in cartItems)
            {
                // Membuat Border untuk tiap item cart
                var border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 10, 0, 0),
                    Background = Brushes.White
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                // Menampilkan nama dan harga produk
                var itemName = new TextBlock { Text = item.ProductName, FontWeight = FontWeights.Bold, FontSize = 16 };
                var itemPrice = new TextBlock { Text = $"Price: {item.UnitPrice:C}", FontWeight = FontWeights.Bold, Foreground = Brushes.Green, FontSize = 14 };

                // Mengisi kolom grid dengan item
                Grid.SetColumn(itemName, 0);
                Grid.SetColumn(itemPrice, 1);
                grid.Children.Add(itemName);
                grid.Children.Add(itemPrice);

                // Menambahkan grid ke dalam border, dan border ke panel CartItemsPanel
                border.Child = grid;
                CartItemsPanel.Children.Add(border);
            }
        }
    }
}
