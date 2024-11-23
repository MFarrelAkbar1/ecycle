using Ecycle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;

namespace Ecycle.Pages
{
    public partial class UserProduct : Page
    {
        private readonly string userProductsEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/user/";
        private readonly string createProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/post";
        private readonly string updateProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/update";
        private readonly string deleteProductEndpoint = "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/delete";

        private List<UserProductModel> products;
        private UserProductModel currentProduct;

        public UserProduct()
        {
            InitializeComponent();
            LoadProducts(); // Memuat produk saat halaman diinisialisasi
        }

        private async void LoadProducts()
        {
            try
            {
                using var httpClient = new HttpClient();
                var userId = UserSession.PenggunaID; // Ensure the ID is not null or empty
                var url = $"{userProductsEndpoint}{UserSession.PenggunaID}";

                // Show the URL in a MessageBox for debugging
                MessageBox.Show($"Fetching products from: {url}", "Debug Info", MessageBoxButton.OK, MessageBoxImage.Information);

                var response = await httpClient.GetStringAsync(url);

                // Show the raw response in a MessageBox for debugging
                MessageBox.Show($"Response: {response}", "API Response", MessageBoxButton.OK, MessageBoxImage.Information);

                products = JsonConvert.DeserializeObject<List<UserProductModel>>(response);
                ProductsList.ItemsSource = products;
            }
            catch (Exception ex)
            {
                // Show error in a MessageBox
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack(); // Kembali ke halaman sebelumnya
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
            ProductForm.Visibility = Visibility.Visible; // Tampilkan form input produk baru
            btnUpdate.Visibility = Visibility.Collapsed; // Sembunyikan tombol Update
            btnSave.Visibility = Visibility.Visible;     // Tampilkan tombol Save
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            currentProduct = button?.DataContext as UserProductModel;

            if (currentProduct != null)
            {
                // Isi form dengan data produk yang dipilih
                FormTitle.Text = "Edit Product";
                txtProductName.Text = currentProduct.Nama;
                txtProductDescription.Text = currentProduct.Deskripsi;
                txtProductPrice.Text = currentProduct.Harga?.ToString();
                txtProductStock.Text = currentProduct.Stok?.ToString();

                ProductForm.Visibility = Visibility.Visible; // Tampilkan form edit
                btnUpdate.Visibility = Visibility.Visible;   // Tampilkan tombol Update
                btnSave.Visibility = Visibility.Collapsed;   // Sembunyikan tombol Save
            }
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
            // Validation
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductDescription.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtProductStock.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Parse numeric values
                if (!int.TryParse(txtProductStock.Text, out int stock))
                {
                    MessageBox.Show("Stock must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtProductPrice.Text, out int price))
                {
                    MessageBox.Show("Price must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create new product with all fields
                var newProduct = new
                {
                    nama = txtProductName.Text,
                    deskripsi = txtProductDescription.Text,  // Make sure this is included
                    stok = stock,                            // Use the parsed integer value
                    terjual = 0,                            // Initialize with 0
                    harga = price,
                    ongkosKirim = 0,                        // Initialize with 0 or null
                    kategoriID = (int?)null,                // Keep as null if not needed
                    penjualID = UserSession.PenggunaID,     // Include the seller ID
                    bahanID = (int?)null                    // Keep as null if not needed
                };

                using var httpClient = new HttpClient();

                // Set content type header
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize and log the request body for debugging
                var json = JsonConvert.SerializeObject(newProduct);
                Console.WriteLine($"Request body: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Log the URL being called
                Console.WriteLine($"Calling endpoint: {createProductEndpoint}");

                var response = await httpClient.PostAsync(createProductEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Log the response
                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts(); // Refresh the product list
                    ProductForm.Visibility = Visibility.Collapsed; // Hide the input form
                    ResetForm(); // Clear the form
                }
                else
                {
                    MessageBox.Show($"Error saving product. Status: {response.StatusCode}\nResponse: {responseContent}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception details: {ex}");
                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct == null) return;

            currentProduct.Nama = txtProductName.Text;
            currentProduct.Deskripsi = txtProductDescription.Text;
            currentProduct.Stok = int.Parse(txtProductStock.Text);
            currentProduct.Harga = int.Parse(txtProductPrice.Text);

            try
            {
                using var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(currentProduct);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PatchAsync(updateProductEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts(); // Perbarui daftar produk
                    ProductForm.Visibility = Visibility.Collapsed; // Sembunyikan form input
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error updating product: {errorResponse}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button?.DataContext as UserProductModel;
            if (product == null)
            {
                MessageBox.Show("No product selected for deletion.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate user session with correct case for Password
            if (string.IsNullOrEmpty(UserSession.Username) || string.IsNullOrEmpty(UserSession.Password))
            {
                MessageBox.Show(
                    "You must be logged in to delete products. Please log in again.",
                    "Authentication Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            var confirmation = MessageBox.Show(
                $"Are you sure you want to delete product '{product.Nama}' (ID: {product.ProdukID})?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirmation != MessageBoxResult.Yes) return;

            try
            {
                using var httpClient = new HttpClient();

                // Add authentication headers
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the request body with proper case for Username and Password
                var deleteData = new
                {
                    produkID = product.ProdukID,
                    Username = UserSession.Username,  // Correct case
                    Password = UserSession.Password,  // Correct case
                    penjualID = UserSession.PenggunaID
                };

                // Create DELETE request with the properly cased data
                var request = new HttpRequestMessage(HttpMethod.Delete,
                    "https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/product/delete");

                var json = JsonConvert.SerializeObject(deleteData);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                // Log for debugging
                Console.WriteLine($"Request URL: {request.RequestUri}");
                Console.WriteLine($"Request content: {json}");

                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Product deleted successfully!",
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    LoadProducts(); // Reload the product list
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to delete product.\nStatus: {response.StatusCode}\nResponse: {responseContent}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception details: {ex}");
                MessageBox.Show(
                    $"Error deleting product:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}