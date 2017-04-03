namespace NG.FC_DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloAula : DbContext
    {
        public ModeloAula()
            : base("name=ModeloAula")
        {
        }

        public virtual DbSet<CATALOG_DEFINITION> CATALOG_DEFINITION { get; set; }
        public virtual DbSet<CATALOG_DETAILS> CATALOG_DETAILS { get; set; }
        public virtual DbSet<SCHOOL_ATTENDANCE> SCHOOL_ATTENDANCE { get; set; }
        public virtual DbSet<SCHOOL_SUBJECTS> SCHOOL_SUBJECTS { get; set; }
        public virtual DbSet<STUDENT> STUDENTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATALOG_DEFINITION>()
                .Property(e => e.DETAILS)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DEFINITION>()
                .Property(e => e.MAKER)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DEFINITION>()
                .HasMany(e => e.CATALOG_DETAILS)
                .WithRequired(e => e.CATALOG_DEFINITION)
                .HasForeignKey(e => e.ID_CATALOG_DEFINITION)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.MAKER)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD0)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD1)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD2)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD3)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD4)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD5)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD6)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD7)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD8)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD9)
                .IsUnicode(false);

            modelBuilder.Entity<CATALOG_DETAILS>()
                .Property(e => e.FIELD10)
                .IsUnicode(false);

            modelBuilder.Entity<SCHOOL_ATTENDANCE>()
                .Property(e => e.MAKER)
                .IsUnicode(false);

            modelBuilder.Entity<SCHOOL_SUBJECTS>()
                .Property(e => e.MAKER)
                .IsUnicode(false);

            modelBuilder.Entity<SCHOOL_SUBJECTS>()
                .Property(e => e.RATING_RECORD)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SCHOOL_SUBJECTS>()
                .HasMany(e => e.SCHOOL_ATTENDANCE)
                .WithRequired(e => e.SCHOOL_SUBJECTS)
                .HasForeignKey(e => e.ID_SCHOOL_SUBJECTS);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.MAKER)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.ALTERNATE_MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.APARTMENT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.ID_CAT_DET_CP)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.PWD)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.TOKEN)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.HOME_REFERENCE)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.LASTNAME)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.TELEPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.TELEPHONE2)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .HasMany(e => e.SCHOOL_SUBJECTS)
                .WithRequired(e => e.STUDENT)
                .HasForeignKey(e => e.ID_STUDENT);
        }
    }
}
