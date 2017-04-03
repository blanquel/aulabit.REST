using System.ComponentModel.DataAnnotations;

namespace aulabit.frontend.Models
{
    public class LoginModel
    {

        private string email;


        [Required(ErrorMessage = "Ingrese Email")]
        [EmailAddress(ErrorMessage = "Ingrese un Email valido")]
        public string Email
        {
            get { return email; }
            set { email = value.ToString(); }
        }

        [Required(ErrorMessage = "Ingrese Contraseña")]
        public string PWD { get; set; }

    }
}