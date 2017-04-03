using System.ComponentModel.DataAnnotations;
namespace NG.ViewModels
{
    public class SchoolSubjectVM
    {
        [Required(ErrorMessage = "* Id es requerido:0 registro nuevo , <0 id registro a manipular")]
        public int Id { get; set; }
        public string MAKER { get; set; } = "MASTER"; 
        public bool Status { get; set; } = true;

        [Range(0, 10.0, ErrorMessage = "* Calificación debe ser entre 0 y 10")]
        public decimal RatingRecord { get; set; }

        [Required(ErrorMessage = "* Debe elegir una materia escolar Hábil")]
        public int IdCatDet { get; set; }

        [Required(ErrorMessage = "* Debe elegir un estudiante Hábil ")]
        public int IdStudent { get; set; }

    }
}
