using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainDeriven
{
    [Table("ProjectFeature")]
    public partial class ProjectFeature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectFeature()
        {
            ProjectFeature1 = new HashSet<ProjectFeature>();
        }
        public int Id { get; set; }

        public int? ProjectId { get; set; }
        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool? IsEnglish { get; set; }

        public int? EnId { get; set; }

        public byte? Order { get; set; }

        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectFeature> ProjectFeature1 { get; set; }

        public virtual ProjectFeature ProjectFeature2 { get; set; }
    }
}
