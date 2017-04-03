namespace NG.FC_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CATALOG_DETAILS
    {
        public int ID { get; set; }

        public int ID_CATALOG_DEFINITION { get; set; }

        public int ROW_ITEM { get; set; }

        public bool? STATUS_ITEM { get; set; }

        public DateTime CREATION_DATE { get; set; }=DateTime.Now;

        public DateTime MODIFICATION_DATE { get; set; } = DateTime.Now;

        [Required]
        [StringLength(150)]
        public string MAKER { get; set; } = "MASTER";

        [StringLength(500)]
        public string FIELD0 { get; set; }

        public string FIELD1 { get; set; }

        [StringLength(500)]
        public string FIELD2 { get; set; }

        [StringLength(500)]
        public string FIELD3 { get; set; }

        [StringLength(500)]
        public string FIELD4 { get; set; }

        [StringLength(500)]
        public string FIELD5 { get; set; }

        [StringLength(500)]
        public string FIELD6 { get; set; }

        [StringLength(500)]
        public string FIELD7 { get; set; }

        [StringLength(500)]
        public string FIELD8 { get; set; }

        [StringLength(500)]
        public string FIELD9 { get; set; }

        [StringLength(500)]
        public string FIELD10 { get; set; }

        public virtual CATALOG_DEFINITION CATALOG_DEFINITION { get; set; }
    }
}
