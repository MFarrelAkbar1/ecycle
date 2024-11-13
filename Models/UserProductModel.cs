namespace Ecycle.Models
{
    public class Product
    {
        public int ProdukID { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public int Stok { get; set; }
        public int Harga { get; set; }
        public int OngkosKirim { get; set; }
        public int KategoriID { get; set; }
        public int PenjualID { get; set; }
        public int BahanID { get; set; }
    }
}
