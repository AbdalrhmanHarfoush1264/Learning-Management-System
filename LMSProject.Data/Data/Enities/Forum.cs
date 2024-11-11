namespace LMSProject.Data.Data.Enities
{
    public class Forum
    {
        public int ForumId { get; set; }
        public string Title { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public ICollection<ForumPost>? ForumPosts { get; set; } = new HashSet<ForumPost>();
    }
}

