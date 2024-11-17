﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecycle.Models
{
    public class ArticleModel
    {
        [JsonProperty("artikelID")]
        public int ArtikelID { get; set; }
        [JsonProperty("judul")]
        public string Judul { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        [JsonProperty("konten")]
        public string Konten { get; set; } = string.Empty;
        [JsonProperty("adminID")]
        public int AdminID { get; set; } = 0;
        public DateTime PublishedDate { get; set; }
    }
}