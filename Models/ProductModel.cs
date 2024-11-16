using Newtonsoft.Json;
using System;
public class ProductModel
{
    [JsonProperty("produkID")]
    public int ProdukID { get; set; }
    [JsonProperty("nama")]
    public string Name { get; set; }
    [JsonProperty("deskripsi")]
    public string Description { get; set; }
    [JsonProperty("stok")]
    public int Stock { get; set; }
    [JsonProperty("terjual")]
    public int Sold { get; set; }
    [JsonProperty("harga")]
    public decimal Price { get; set; }
    [JsonProperty("ongkosKirim")]
    public int ShippingCost { get; set; }
}

