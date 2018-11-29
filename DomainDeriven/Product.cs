namespace DomainDeriven
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Product1 = new HashSet<Product>();
            ProductFeatures = new HashSet<ProductFeature>();
        }

        public int Id { get; set; }

        public int? ProductCategoryId { get; set; }

        [StringLength(500)]
        public string Brand { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public bool? IsEnglish { get; set; }

        [StringLength(2000)]
        public string ImportantTip1 { get; set; }

        [StringLength(2000)]
        public string ImportantTip2 { get; set; }

        [StringLength(2000)]
        public string ImportantTip3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public int? EnId { get; set; }

        [StringLength(500)]
        public string ImageFolderName { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product1 { get; set; }

        public virtual Product Product2 { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
    }
}
