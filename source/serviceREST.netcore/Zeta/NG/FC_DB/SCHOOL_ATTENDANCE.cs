namespace NG.FC_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SCHOOL_ATTENDANCE
    {
        public int ID { get; set; }

        public int ID_SCHOOL_SUBJECTS { get; set; }

        public DateTime CREATE_DATE { get; set; } = DateTime.Now;

        public DateTime MODIFICATION_DATE { get; set; } = DateTime.Now;

        [Required]
        [StringLength(150)]
        public string MAKER { get; set; } = "MASTER";

        public bool STATUS_ITEM { get; set; }

        public DateTime ATTENDANCE_RECORD { get; set; }

        public virtual SCHOOL_SUBJECTS SCHOOL_SUBJECTS { get; set; }
    }
}
