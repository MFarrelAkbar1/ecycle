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

        // Menampilkan detail produk
        public void LoadProductDetails(ProductModel product)
        {
            txtProductName.Text = product.Name;
            txtProductSold.Text = $"Sold: {product.Sold}";
            txtProductStock.Text = $"Available stock: {product.Stock}";
            txtProductPrice.Text = $"{product.Price:C}";
            txtProductDescription.Text = product.Description;
            txtProductShipping.Text = "Shipping from Yogyakarta"; // Misal informasi pengiriman
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
