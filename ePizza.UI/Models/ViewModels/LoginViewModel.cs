using System.ComponentModel.DataAnnotations;

namespace ePizza.UI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName {  get; set; } = default!;

        [Required]
        [MinLength(5, ErrorMessage = "Minimum Length of password should be 5 characters")]
        [MaxLength(20, ErrorMessage = "Maximum Length of password should be 20 characters")]
        public string Password { get; set; } = default!;
    }
}
