using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecycle.Models
{
    public class UserProductModel
    {
        public int ProductId { get; set; }        // produkID
        public string Name { get; set; }          // nama
        public string Description { get; set; }   // deskripsi
        public int Stock { get; set; }            // stok
        public decimal Price { get; set; }        // harga
        public int ShippingCost { get; set; }     // ongkosKirim
        public int CategoryId { get; set; }       // kategoriID
        public int SellerId { get; set; }         // penjualID
        public int MaterialId { get; set; }       // bahanID
    }
}

