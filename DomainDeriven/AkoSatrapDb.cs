namespace DomainDeriven
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AkoSatrapDb : DbContext
    {
        public AkoSatrapDb()
            : base("name=AkoSatrapDb")
        {
        }

        public virtual DbSet<PictureGallery> PictureGalleries { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductFeature> ProductFeatures { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<ProjectFeature> ProjectFeatures { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PictureGallery>()
                .Property(e => e.ImageFolderName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ImageFolderName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product1)
                .WithOptional(e => e.Product2)
                .HasForeignKey(e => e.EnId);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory1)
                .WithOptional(e => e.ProductCategory2)
                .HasForeignKey(e => e.EnId);

            modelBuilder.Entity<ProductFeature>()
                .HasMany(e => e.ProductFeature1)
                .WithOptional(e => e.ProductFeature2)
                .HasForeignKey(e => e.EnId);

            modelBuilder.Entity<Project>()
                .Property(e => e.ImageFolderName)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Project1)
                .WithOptional(e => e.Project2)
                .HasForeignKey(e => e.EnId);

            modelBuilder.Entity<ProjectCategory>()
                .HasMany(e => e.ProjectCategory1)
                .WithOptional(e => e.ProjectCategory2)
                .HasForeignKey(e => e.EnId);

            modelBuilder.Entity<ProjectFeature>()
                .HasMany(e => e.ProjectFeature1)
                .WithOptional(e => e.ProjectFeature2)
                .HasForeignKey(e => e.EnId);
        }
    }
}
