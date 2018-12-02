using System.Collections.Generic;

namespace DomainDeriven
{
    public partial class ProjectFeature
    {
        public int Id { get; set; }

        public int? ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool? IsEnglish { get; set; }

        public int? EnId { get; set; }

        public byte? Order { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<ProjectFeature> ProjectFeature1 { get; set; }

        public virtual ProjectFeature ProjectFeature2 { get; set; }
    }
}
