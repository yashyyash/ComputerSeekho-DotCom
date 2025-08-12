using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchCourse> BatchCourses { get; set; }
        public DbSet<CampusLife> CampusLife { get; set; }
        public DbSet<ClosureReason> ClosureReasons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<GetInTouch> GetInTouch { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for BatchCourse
            modelBuilder.Entity<BatchCourse>()
                .HasKey(bc => new { bc.BatchId, bc.CourseId });

            modelBuilder.Entity<BatchCourse>()
                .HasOne(bc => bc.Batch)
                .WithMany(b => b.BatchCourses)
                .HasForeignKey(bc => bc.BatchId);

            modelBuilder.Entity<BatchCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.BatchCourses)
                .HasForeignKey(bc => bc.CourseId);

            // Decimal precision settings
            modelBuilder.Entity<Course>()
                .Property(c => c.CourseFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

             


            modelBuilder.Entity<Student>()
                .Property(s => s.DueAmount)
                .HasPrecision(18, 2);
        }
    }
}
