namespace NG.FC_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CATALOG_DEFINITION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATALOG_DEFINITION()
        {
            CATALOG_DETAILS = new HashSet<CATALOG_DETAILS>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public DateTime DATE_ISSUE { get; set; } = DateTime.Now;

        public DateTime DATE_UPDATE { get; set; } = DateTime.Now;

        [Required]
        [StringLength(500)]
        public string DETAILS { get; set; }

        [Required]
        [StringLength(150)]
        public string MAKER { get; set; } = "MASTER";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATALOG_DETAILS> CATALOG_DETAILS { get; set; }
    }
}
