namespace E_learning_platform.DTOs.Responses
{
    public class PagedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;
        public IEnumerable<T> Data { get; set; }

        public PagedResponse(IEnumerable<T> data, int count, int page, int pageSize)
        {
            Data = data;
            TotalItems = count;
            PageSize = pageSize;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
