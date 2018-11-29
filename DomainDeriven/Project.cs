namespace DomainDeriven
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Project1 = new HashSet<Project>();
            ProjectFeatures = new HashSet<ProjectFeature>();
        }

        public int Id { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(500)]
        public string Province { get; set; }

        [StringLength(500)]
        public string City { get; set; }

        public bool? IsEnglish { get; set; }

        public int? EnId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public double? CompletionPercentage { get; set; }

        [StringLength(500)]
        public string master { get; set; }

        public int? ProjectCategoryId { get; set; }

        [StringLength(500)]
        public string ImageFolderName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project1 { get; set; }

        public virtual Project Project2 { get; set; }

        public virtual ProjectCategory ProjectCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectFeature> ProjectFeatures { get; set; }
    }
}
