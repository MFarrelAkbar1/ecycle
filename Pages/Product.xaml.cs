using System.Windows;
using System.Windows.Controls;
using Ecycle.Models; 

namespace Ecycle.Pages
{
    public partial class Product : Page
    {
        public Product()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void LoadProductDetails(ProductModel product)
        {
            txtProductName.Text = product.Name;
            txtProductSold.Text = $"Sold: {product.Sold}";
            txtProductStock.Text = $"Available stock: {product.Stock}";
            txtProductPrice.Text = $"{product.Price:C}";
            txtProductDescription.Text = product.Description;
            txtProductShipping.Text = "Shipping from Surabaya"; // Example shipping info
            txtProductSeller.Text = product.SellerName ?? "Unknown Seller"; // Handle null seller name
            txtSellerLocation.Text = product.SellerLocation ?? "Location not specified"; // Handle null seller location
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
