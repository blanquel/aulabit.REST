using System;
using System.ComponentModel.DataAnnotations;

namespace NG.ViewModels
{
    public class SchoolAttendaceVM
    {
        [Required(ErrorMessage = "* Id es requerido : 0 registro nuevo , < 0 id registro a manipular")]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Id de materia es requerido")]
        public int IdSchoolSubject { get; set; }

        public bool StatusItem { get; set; } = true;


        [DataType(DataType.DateTime, ErrorMessage = "* Fecha invalida")]
        public DateTime AttendanceRecord { get; set; }

        public string MAKER { get; set; } = "MASTER";

    }


}
