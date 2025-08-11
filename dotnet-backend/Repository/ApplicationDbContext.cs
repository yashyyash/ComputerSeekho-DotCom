using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor for runtime DI
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Parameterless constructor for design-time usage
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=computerseekhodotnetnew;user=root;password=Kushal@22;SslMode=none;";
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
            }
        }

        // Your DbSets and OnModelCreating remain unchanged
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchCourse> BatchCourses { get; set; }
        public DbSet<CampusLife> CampusLives { get; set; }
        public DbSet<ClosureReason> ClosureReasons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<GetInTouch> GetInTouches { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentInstallment> PaymentInstallments { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Student> Students { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<BatchCourse>()
        //        .HasKey(bc => new { bc.BatchId, bc.CourseId });

        //    modelBuilder.Entity<BatchCourse>()
        //        .HasOne(bc => bc.Batch)
        //        .WithMany(b => b.BatchCourses)
        //        .HasForeignKey(bc => bc.BatchId);

        //    modelBuilder.Entity<BatchCourse>()
        //        .HasOne(bc => bc.Course)
        //        .WithMany(c => c.BatchCourses)
        //        .HasForeignKey(bc => bc.CourseId);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing composite key for BatchCourse
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

            // Specify precision and scale for decimal properties
            modelBuilder.Entity<Course>()
                .Property(c => c.CourseFee)
                .HasPrecision(18, 2);  // or adjust precision and scale as needed

            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PaymentInstallment>()
                .Property(pi => pi.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Student>()
                .Property(s => s.DueAmount)
                .HasPrecision(18, 2);
        }

    }
}
