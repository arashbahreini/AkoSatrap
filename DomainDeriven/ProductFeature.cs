namespace DomainDeriven
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductFeature")]
    public partial class ProductFeature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductFeature()
        {
            ProductFeature1 = new HashSet<ProductFeature>();
        }

        public int Id { get; set; }

        public int? ProductId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        public byte? Order { get; set; }

        public int? EnId { get; set; }

        public bool? IsEnglish { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFeature> ProductFeature1 { get; set; }

        public virtual ProductFeature ProductFeature2 { get; set; }
    }
}
