using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ecycle.Models;

namespace Ecycle.Pages
{
    public partial class Cart : Page
    {
        public Cart()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (CartStorage.Items.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add some products!", "Cart Empty", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            LoadCartItems();
            DisplayTotalPrice();
        }

        private void LoadCartItems()
        {
            CartItemsPanel.Children.Clear();

            foreach (var item in CartStorage.Items)
            {
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
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                var itemName = new TextBlock { Text = $"{item.ProductName} (x{item.Quantity})", FontWeight = FontWeights.Bold, FontSize = 16 };
                var itemPrice = new TextBlock { Text = $"Total: {item.TotalPrice:C}", FontWeight = FontWeights.Bold, Foreground = Brushes.Green, FontSize = 14 };

                // Quantity Controls
                var quantityPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
                var decreaseButton = new Button { Content = "-", Width = 20, Height = 20, Margin = new Thickness(5, 0, 5, 0) };
                decreaseButton.Click += (s, e) => DecreaseQuantity(item);

                var quantityText = new TextBlock { Text = item.Quantity.ToString(), Width = 30, TextAlignment = TextAlignment.Center };

                var increaseButton = new Button { Content = "+", Width = 20, Height = 20, Margin = new Thickness(5, 0, 5, 0) };
                increaseButton.Click += (s, e) => IncreaseQuantity(item);

                quantityPanel.Children.Add(decreaseButton);
                quantityPanel.Children.Add(quantityText);
                quantityPanel.Children.Add(increaseButton);

                var deleteButton = new Button
                {
                    Content = "X",
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Width = 25,
                    Height = 25,
                    FontSize = 12,
                    Margin = new Thickness(5, 0, 0, 0),
                    Padding = new Thickness(0)
                };
                deleteButton.Click += (s, e) => DeleteItem(item);

                var itemDetailsPanel = new StackPanel();
                itemDetailsPanel.Children.Add(itemName);
                itemDetailsPanel.Children.Add(itemPrice);
                itemDetailsPanel.Children.Add(quantityPanel);

                grid.Children.Add(itemDetailsPanel);
                grid.Children.Add(deleteButton);
                Grid.SetColumn(deleteButton, 1);

                border.Child = grid;
                CartItemsPanel.Children.Add(border);
            }
        }

        private void IncreaseQuantity(CartItemModel item)
        {
            item.Quantity++;
            LoadCartItems();
            DisplayTotalPrice();
        }

        private void DecreaseQuantity(CartItemModel item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                LoadCartItems();
                DisplayTotalPrice();
            }
            else
            {
                MessageBox.Show("Minimum quantity is 1.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteItem(CartItemModel item)
        {
            CartStorage.Items.Remove(item);
            LoadCartItems();
            DisplayTotalPrice();
        }

        private void DisplayTotalPrice()
        {
            decimal totalPrice = CartStorage.Items.Sum(item => item.TotalPrice);
            TotalPriceText.Text = $"Total Price: {totalPrice:C}";
        }
    }
}
