namespace LMSProject.Bussiness.Dtos.ForumDTOS
{
    public class AddForumRequest
    {
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
    }
}
