using System.Data.Entity;

namespace DataAnnotations
{
    public class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("name=PlutoContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
               .Property(a => a.Name)
               .IsRequired()
               .HasMaxLength(255);

            modelBuilder.Entity<Course>()
                .Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(2000);

            modelBuilder.Entity<Course>()
                .HasRequired(a => a.Author)
                .WithMany(a => a.Courses)
                .HasForeignKey(a => a.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(a => a.Tags)
                .WithMany(a => a.Courses)
                .Map(m => 
                   {
                       m.ToTable("CourseTags");
                       m.MapLeftKey("CourseId");
                       m.MapRightKey("TagId");
                   });


            modelBuilder.Entity<Course>()
                .HasRequired(c => c.Cover)
                .WithRequiredPrincipal(c => c.course);

            base.OnModelCreating(modelBuilder);
        }
    }
}