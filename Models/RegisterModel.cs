using System.ComponentModel.DataAnnotations;

namespace WebAppASPNET.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Имя должно содержать от 2 до 50 символов")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$",
            ErrorMessage = "Имя может содержать только буквы, цифры и дефисы")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        [StringLength(100,
            ErrorMessage = "Email не может превышать 100 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Пароль должен содержать минимум 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MustBeTrue(ErrorMessage = "Необходимо принять условия соглашения")]
        public bool AgreeToTerms { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
    }
}