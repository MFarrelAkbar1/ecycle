using System;
using System.Windows;
using System.Windows.Controls;

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        private bool isEditMode = false;
        private string? editingProductId;

        public UserProduct()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to Account page
            NavigationService?.Navigate(new Account());
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            SetFormForAdd();
            ProductForm.Visibility = Visibility.Visible;
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            isEditMode = true;
            editingProductId = "ID_PRODUK";
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            FormTitle.Text = "Update Product";
            btnSave.Content = "Update Product";
            ProductForm.Visibility = Visibility.Visible;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Product deleted successfully!", "Delete Product", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                MessageBox.Show("Product updated successfully!", "Update Product", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Product added successfully!", "Add Product", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
            }
            ProductForm.Visibility = Visibility.Collapsed;
        }

        private void SetFormForAdd()
        {
            isEditMode = false;
            editingProductId = null;
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            FormTitle.Text = "Add New Product";
            btnSave.Content = "Add Product";
        }

        private void ResetForm()
        {
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            FormTitle.Text = "Add New Product";
            btnSave.Content = "Add Product";
            isEditMode = false;
        }
    }
}
