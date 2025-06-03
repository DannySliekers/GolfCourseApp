using GolfCourseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseWebAPI.Context
{
    public class GolfCourseContext : DbContext
    {
        public DbSet<GolfCourse> golf_courses { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<GolfCourseImage> golf_course_images { get; set; }

        private readonly string _connectionString;

        public GolfCourseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB") ?? throw new NullReferenceException("Connection String");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(u => u.role)
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => Enum.Parse<UserRole>(v, true)
                );
        }
    }
}
