namespace DomainDeriven
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectCategory")]
    public partial class ProjectCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectCategory()
        {
            Projects = new HashSet<Project>();
            ProjectCategory1 = new HashSet<ProjectCategory>();
        }

        public int Id { get; set; }

        public bool? IsEnglish { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public int? EnId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategory> ProjectCategory1 { get; set; }

        public virtual ProjectCategory ProjectCategory2 { get; set; }
    }
}
