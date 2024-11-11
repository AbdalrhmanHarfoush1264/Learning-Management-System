namespace LMSProject.Data.Bases
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string>? Messages { get; set; } = new();
        public bool Successed { get; set; }

        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedResult(bool successed, List<T> data = default, List<string> messages = null,
            int totalCount = 0, int page = 1, int pageSize = 10)
        {
            Successed = successed;
            Data = data;
            Messages = messages;
            TotalCount = totalCount;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }

    }
}
