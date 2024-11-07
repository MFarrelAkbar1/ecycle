using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecycle.Models
{
    public class LoginResultModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
        public AccountModel? Account { get; set; } // Nullable for failed logins
    }
}
