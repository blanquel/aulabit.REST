namespace NG.FC_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SCHOOL_SUBJECTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCHOOL_SUBJECTS()
        {
            SCHOOL_ATTENDANCE = new HashSet<SCHOOL_ATTENDANCE>();
        }

        public int ID { get; set; }

        public DateTime CREATE_DATE { get; set; } = DateTime.Now;

        public DateTime MODIFICATION_DATE { get; set; } = DateTime.Now;

        [Required]
        [StringLength(150)]
        public string MAKER { get; set; } = "MASTER";

        public bool STATUS_ITEM { get; set; }

        public decimal RATING_RECORD { get; set; }

        public int ID_CAT_DET { get; set; }

        public int ID_STUDENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHOOL_ATTENDANCE> SCHOOL_ATTENDANCE { get; set; }

        public virtual STUDENT STUDENT { get; set; }
    }
}
