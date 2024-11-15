using Ecycle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        private readonly string userProductsEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/user/{0}";
        private readonly string createProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/post";
        private readonly string updateProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/update";
        private readonly string deleteProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/{0}";

        private List<UserProductModel> products; // Menyimpan daftar produk
        private UserProductModel currentProduct; // Menyimpan produk saat ini yang akan diedit

        public UserProduct()
        {
            InitializeComponent();
            LoadProducts(); // Memuat produk saat halaman diinisialisasi
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // Kembali ke halaman sebelumnya
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ResetForm(); // Reset form saat menambah produk baru
            ProductForm.Visibility = Visibility.Visible; // Tampilkan form
            btnUpdate.Visibility = Visibility.Collapsed; // Sembunyikan tombol Update
            btnSave.Visibility = Visibility.Visible; // Tampilkan tombol Save
        }

        private void ResetForm()
        {
            txtProductName.Text = string.Empty;
            txtProductDescription.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductStock.Text = string.Empty;
            currentProduct = null; // Reset produk yang sedang di-edit
        }

        private async void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductDescription.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtProductStock.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newProduct = new UserProductModel
            {
                Nama = txtProductName.Text,
                Deskripsi = txtProductDescription.Text,
                Stok = int.TryParse(txtProductStock.Text, out var stock) ? stock : (int?)null,
                Harga = int.TryParse(txtProductPrice.Text, out var price) ? price : (int?)null,
                OngkosKirim = 0,
                KategoriID = 0,
                PenjualID = 1, // Ganti dengan ID pengguna yang sesuai
                BahanID = 0
            };

            bool isSuccessful = await CreateProductAsync(newProduct);
            if (isSuccessful)
            {
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadProducts(); // Muat ulang produk
                ProductForm.Visibility = Visibility.Collapsed; // Sembunyikan form
            }
            else
            {
                MessageBox.Show("Failed to create product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> CreateProductAsync(UserProductModel product)
        {
            try
            {
                using var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(createProductEndpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private async void LoadProducts()
        {
            try
            {
                using var httpClient = new HttpClient();
                var userId = 1; // Ganti dengan ID pengguna yang sesuai
                var response = await httpClient.GetStringAsync(string.Format(userProductsEndpoint, userId));
                products = JsonConvert.DeserializeObject<List<UserProductModel>>(response);
                ProductsList.ItemsSource = products; // Bind produk ke kontrol
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            currentProduct = button.DataContext as UserProductModel; // Ambil produk yang dipilih
            if (currentProduct != null)
            {
                // Isi form dengan detail produk yang ada
                FormTitle.Text = "Edit Product"; // Ganti judul form
                txtProductName.Text = currentProduct.Nama;
                txtProductDescription.Text = currentProduct.Deskripsi;
                txtProductPrice.Text = currentProduct.Harga.ToString();
                txtProductStock.Text = currentProduct.Stok.ToString();
                ProductForm.Visibility = Visibility.Visible; // Tampilkan form untuk edit

                // Tombol yang terlihat saat mengedit
                btnUpdate.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Collapsed; // Sembunyikan tombol untuk menyimpan produk baru
            }
        }

        private async void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct == null)
            {
                MessageBox.Show("No product selected for update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update informasi produk yang ada
            currentProduct.Nama = txtProductName.Text;
            currentProduct.Deskripsi = txtProductDescription.Text;
            currentProduct.Harga = int.TryParse(txtProductPrice.Text, out var price) ? price : (int?)null;
            currentProduct.Stok = int.TryParse(txtProductStock.Text, out var stock) ? stock : (int?)null;

            bool isSuccessful = await UpdateProductAsync(currentProduct);
            if (isSuccessful)
            {
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadProducts(); // Muat ulang produk
                ProductForm.Visibility = Visibility.Collapsed; // Sembunyikan form jika berhasil
            }
            else
            {
                MessageBox.Show("Failed to update product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> UpdateProductAsync(UserProductModel product)
        {
            try
            {
                using var httpClient = new HttpClient();
                var updateProduk = new
                {
                   // fill this
                };
                string json = JsonConvert.SerializeObject(updateProduk);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PatchAsync(updateProductEndpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.DataContext as UserProductModel; // Mengambil produk yang dipilih
            if (product != null)
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButton.YesNo);
                if (confirmation == MessageBoxResult.Yes)
                {
                    bool isDeleted = await DeleteProductAsync(product.ProdukID);
                    if (isDeleted)
                    {
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadProducts(); // Muat ulang daftar produk
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async Task<bool> DeleteProductAsync(int produkID)
        {
            try
            {
                using var httpClient = new HttpClient();
                var url = string.Format(deleteProductEndpoint, produkID); // Add produkID to the URL
                var response = await httpClient.DeleteAsync(url); // Use DELETE method
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
