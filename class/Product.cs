using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecycle
{
    internal class Product {
        public int Id { get; private set; }
        public string Nama { get; private set; }
        public string Deskripsi { get; private set; }
        public double Harga { get; private set; }
        public string Kategori { get; private set; }
        public string Bahan { get; private set; }
        public int Stok { get; private set; }
        public int PenggunaID { get; private set; }

        public Product(int id, string nama, string deskripsi, double harga, string kategori, string bahan, int stok, int penggunaId)
        {
            Id = id;
            Nama = nama;
            Deskripsi = deskripsi;
            Harga = harga;
            Kategori = kategori;
            Bahan = bahan;
            Stok = stok;
            PenggunaID = penggunaId;
        }

        public void UpdateProduk(string nama, string deskripsi, double harga, string kategori, string bahan, int stok)
        {
            Nama = nama;
            Deskripsi = deskripsi;
            Harga = harga;
            Kategori = kategori;
            Bahan = bahan;
            Stok = stok;
        }
    }
}
