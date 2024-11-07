namespace Ecycle.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; } // Unique identifier for the product
        public string Name { get; set; } = string.Empty; // Product name
        public string Description { get; set; } = string.Empty; // Product description
        public decimal Price { get; set; } // Product price
        public int Stock { get; set; } // Available stock
        public int Sold { get; set; } // Number of units sold
        public string? SellerName { get; set; } // Nullable for optional fields
        public string? SellerLocation { get; set; } // Nullable for optional fields
    }
}
