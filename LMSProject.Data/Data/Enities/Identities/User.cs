using Microsoft.AspNetCore.Identity;

namespace LMSProject.Data.Data.Enities.Identities
{
    public class User : IdentityUser<int>
    {

        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Country { get; set; }


        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Certificate>? Certificates { get; set; }
        public ICollection<ForumPost>? ForumPosts { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Submission>? Submissions { get; set; }
        public ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
    }
}
