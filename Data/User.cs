using System.ComponentModel.DataAnnotations;

namespace WebAppASPNET.Data
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Мало букв")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AgreeToTerms { get; set; }
    }
}
