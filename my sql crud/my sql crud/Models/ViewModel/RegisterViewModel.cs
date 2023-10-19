using System.ComponentModel.DataAnnotations;

namespace MY_SQL_CRUD.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "UserName")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Only English letters and numbers are allowed.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The username must be between 4 and 50 characters long.h")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Display]
        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).{8,}$", ErrorMessage = "Passwords must have at least one uppercase, one lowercase, one digit, and one special character, and be at least 8 characters long.")]

        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "The  Confirm Password field is required.r")]
        [Compare("Password", ErrorMessage = "Passwords did not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "The  Phone Number field is required.")]
        public string PhoneNumber { get; set; }
    }
}
