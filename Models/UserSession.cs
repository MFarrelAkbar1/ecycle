using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecycle.Models
{
    public static class UserSession
    {
        public static int PenggunaID { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Alamat { get; set; }
        public static string Telepon { get; set; }
        public static string Token { get; set; }
    }
}
