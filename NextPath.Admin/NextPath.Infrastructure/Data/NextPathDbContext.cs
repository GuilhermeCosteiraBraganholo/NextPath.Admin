using Microsoft.EntityFrameworkCore;
using NextPath.Domain.Entities;

namespace NextPath.Infrastructure.Data;

public class NextPathDbContext : DbContext
{
    public NextPathDbContext(DbContextOptions<NextPathDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
    public DbSet<LearningPathCourse> LearningPathCourses => Set<LearningPathCourse>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(builder =>
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
            builder.HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);
        });

        modelBuilder.Entity<Course>(builder =>
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.SkillLevel).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<LearningPath>(builder =>
        {
            builder.Property(lp => lp.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<LearningPathCourse>(builder =>
        {
            modelBuilder.Entity<LearningPathCourse>()
                .HasKey(x => new { x.LearningPathId, x.CourseId });

            builder.HasOne(x => x.LearningPath)
                .WithMany(lp => lp.LearningPathCourses)
                .HasForeignKey(x => x.LearningPathId);

            builder.HasOne(x => x.Course)
                .WithMany(c => c.LearningPathCourses)
                .HasForeignKey(x => x.CourseId);
        });

        modelBuilder.Entity<Enrollment>(builder =>
        {
            builder.HasOne(e => e.Employee)
                .WithMany(emp => emp.Enrollments)
                .HasForeignKey(e => e.EmployeeId);

            builder.HasOne(e => e.LearningPath)
                .WithMany(lp => lp.Enrollments)
                .HasForeignKey(e => e.LearningPathId);
        });
    }
}
