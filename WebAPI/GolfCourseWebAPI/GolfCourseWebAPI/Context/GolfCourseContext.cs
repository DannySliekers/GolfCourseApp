using GolfCourseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseWebAPI.Context
{
    public class GolfCourseContext : DbContext
    {
        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
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

            modelBuilder.Entity<GolfCourse>(entity =>
            {
                entity.ToTable("golf_courses");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OwnerId).HasColumnName("owner_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.BookingStartTime).HasColumnName("booking_start_time");
                entity.Property(e => e.BookingLastStartTime).HasColumnName("booking_last_start_time");
                entity.Property(e => e.StartTimeIntervalMinutes).HasColumnName("start_time_interval_minutes");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
                entity.Property(e => e.GolfCourseId).HasColumnName("golf_course_id");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
            });

            modelBuilder.Entity<User>()
            .Property(u => u.role)
            .HasConversion(
                v => v.ToString().ToLower(),
                v => Enum.Parse<UserRole>(v, true)
            );
        }
    }
}
