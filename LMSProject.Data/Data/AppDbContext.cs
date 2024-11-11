using LMSProject.Data.Data.Enities;
using LMSProject.Data.Data.Enities.Config;
using LMSProject.Data.Data.Enities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMSProject.Data.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
        }
    }
}
