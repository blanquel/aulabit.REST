using System.ComponentModel.DataAnnotations;
namespace NG.ViewModels
{
    public class LoginVM
    {
        [EmailAddress(ErrorMessage = "*Verificar email")]
        public string Email { get; set; }

        private string _pwd;

        [Required(ErrorMessage = "*Es necesario una contraseña")]
        public string PWD
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

    }
}
