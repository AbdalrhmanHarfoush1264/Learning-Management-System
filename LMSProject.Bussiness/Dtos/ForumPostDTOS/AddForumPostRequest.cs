namespace LMSProject.Bussiness.Dtos.ForumPostDTOS
{
    public class AddForumPostRequest
    {
        public string Content { get; set; } = null!;
        public int ForumId { get; set; }
        public int StudentId { get; set; }
    }
}
