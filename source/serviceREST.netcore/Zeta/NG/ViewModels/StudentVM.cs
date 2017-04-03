using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NG.Utils;

namespace NG.ViewModels
{
    public class StudentVM
    {
        public int ID { get; set; } = 0;

        [Required(ErrorMessage = "true or false")]
        public bool Status { get; set; }
        // miss School enrollment
        [Required(ErrorMessage = "* Es necesario un nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Es necesario un apellido")]
        public string LastName { get; set; }
        public string Token { get; set; }

        [EmailAddress(ErrorMessage = "*Verifique dirección de correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Es necesario una contraseña")]
        public string PWD { get; set; }

    }
}
