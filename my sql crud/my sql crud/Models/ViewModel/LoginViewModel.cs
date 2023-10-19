using System.ComponentModel.DataAnnotations;

namespace MY_SQL_CRUD.Models.ViewModel
{
    public class LoginViewModel
    {

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
