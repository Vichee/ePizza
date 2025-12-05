using System.ComponentModel.DataAnnotations;

namespace ePizza.UI.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Name {  get; set; } = default!;


        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;


        [Required]
        [MinLength(5,ErrorMessage ="Minimum Length of password should be 5 characters")]
        [MaxLength(20, ErrorMessage = "Maximum Length of password should be 20 characters")]
        public string Password { get; set; } = default!;


        [Compare("Password",ErrorMessage ="Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; } = default!;

        public string PhoneNumber {  get; set; } = default!;

    }
}
