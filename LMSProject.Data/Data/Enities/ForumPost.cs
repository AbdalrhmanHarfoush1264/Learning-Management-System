using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class ForumPost
    {
        public int ForumPostId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime PostDate { get; set; }


        public int ForumId { get; set; }
        public Forum Forum { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
