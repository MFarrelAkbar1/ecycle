namespace Ecycle.Models
{
    public class AccountModel
    {
        public int penggunaID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Password should be hashed in production
        public string? Address { get; set; } // Nullable for optional address
    }
}
