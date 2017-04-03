namespace NG.FC_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STUDENT")]
    public partial class STUDENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STUDENT()
        {
            SCHOOL_SUBJECTS = new HashSet<SCHOOL_SUBJECTS>();
        }

        public int ID { get; set; }

        public DateTime CREATE_DATE { get; set; } = DateTime.Now;

        public DateTime MODIFICATION_DATE { get; set; } = DateTime.Now;

        [Required]
        [StringLength(150)]
        public string MAKER { get; set; } = "MASTER";

        public bool STATUS_ITEM { get; set; }

        [StringLength(250)]
        public string ALTERNATE_MAIL { get; set; }

        [StringLength(250)]
        public string APARTMENT_NUMBER { get; set; }

        [StringLength(5)]
        public string ID_CAT_DET_CP { get; set; }

        public DateTime DATE_ISSUE { get; set; } = DateTime.Now;

        public DateTime? DATE_DUE { get; set; }

        [Required]
        [StringLength(150)]
        public string EMAIL { get; set; }

        [StringLength(500)]
        public string PWD { get; set; }

        [StringLength(500)]
        public string TOKEN { get; set; }

        public string HOME_REFERENCE { get; set; }

        public int? ID_CAT_DET_IMG { get; set; }

        [Required]
        [StringLength(500)]
        public string NAME { get; set; }

        [Required]
        [StringLength(500)]
        public string LASTNAME { get; set; }

        [StringLength(30)]
        public string TELEPHONE { get; set; }

        [StringLength(30)]
        public string TELEPHONE2 { get; set; }

        public DateTime? YEAR_OF_BIRTH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHOOL_SUBJECTS> SCHOOL_SUBJECTS { get; set; }
    }
}
