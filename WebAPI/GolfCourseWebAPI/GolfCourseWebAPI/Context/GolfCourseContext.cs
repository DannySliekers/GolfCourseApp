using GolfCourseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseWebAPI.Context
{
    public class GolfCourseContext : DbContext
    {
        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GolfCourseImage> GolfCourseImages { get; set; }
        public DbSet<BookingsUsers> BookingsUsers { get; set; }

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

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
                entity.Property(e => e.GolfCourseId).HasColumnName("golf_course_id");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
            });

            modelBuilder.Entity<GolfCourseImage>(entity =>
            {
                entity.ToTable("golf_course_images");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.GolfCourseId).HasColumnName("golf_course_id");
                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("user_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Hash).HasColumnName("hash");
                entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
                entity.Property(e => e.Role).HasColumnName("role").HasConversion(
                    v => v.ToString().ToLower(),
                    v => Enum.Parse<UserRole>(v, true)
                ); 
            });

            modelBuilder.Entity<BookingsUsers>(entity =>
            {
                entity.ToTable("bookings_users");

                entity.HasKey(e => e.UserId);
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
            });
        }
    }
}
