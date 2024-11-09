using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ecycle.Models; // Ensure this namespace is imported

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        public UserProduct()
        {
            InitializeComponent();
            LoadProducts();
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

        private void ResetForm()
        {
            // Clear all form fields
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductStock.Text = string.Empty;
        }

        // Event handler for the TextBox GotFocus event (removes placeholder text)
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var placeholder = (textBox.Parent as Grid)?.Children.OfType<TextBlock>().FirstOrDefault();
            if (placeholder != null && !string.IsNullOrEmpty(textBox.Text))
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
        }

        // Event handler for the TextBox LostFocus event (restores placeholder text if text is empty)
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var placeholder = (textBox.Parent as Grid)?.Children.OfType<TextBlock>().FirstOrDefault();
            if (placeholder != null && string.IsNullOrEmpty(textBox.Text))
            {
                placeholder.Visibility = Visibility.Visible;
            }
        }

        // Placeholder method for LoadProducts
        private void LoadProducts()
        {
            // Logic to load products goes here
        }

        // Event handler for Save Product button (you can implement saving logic here)
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // Logic for saving a new or updated product goes here
        }
    }
}
