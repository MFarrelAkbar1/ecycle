using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecycle.Models
{ 
        public class LoginResponse
        {
            [JsonProperty("penggunaID")]
            public int penggunaID { get; set; }

            [JsonProperty("nama")]
            public string nama { get; set; }

            [JsonProperty("password")]
            public string password { get; set; }

            [JsonProperty("alamat")]
            public string alamat { get; set; }

            [JsonProperty("telepon")]
            public string telepon { get; set; }

            [JsonProperty("token")]
            public string token { get; set; }
        }

}
