using System.ComponentModel.DataAnnotations;

namespace network.Models{
    public class RegisterViewModel{


        [Required]
        [MinLengthAttribute(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Name {get;set;}

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
        [Required]
        public string Description {get;set;}
    }
}