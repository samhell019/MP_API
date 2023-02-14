using Microsoft.EntityFrameworkCore;
using MP_API.Models;

namespace MP_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Drive> Drives { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<User> Users { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //instructors relationships
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            //students relationships
            modelBuilder.Entity<Student>()
                .HasOne(c => c.StudentCourse)
                .WithMany(c => c.Students)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            //drives relationships
            modelBuilder.Entity<Drive>()
                .HasOne(c => c.Instructor)
                .WithMany(c => c.Drives)
                .HasForeignKey(c => c.InstructorId)
                 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Drive>()
                .HasOne(c => c.Student)
                .WithMany(c => c.Drives)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
