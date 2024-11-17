namespace Ecycle.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; } // ID Produk
        public string ProductName { get; set; } // Nama Produk
        public decimal UnitPrice { get; set; } // Harga Satuan
        public int Quantity { get; set; } // Jumlah

        // Menghitung total harga berdasarkan jumlah
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
