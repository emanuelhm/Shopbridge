namespace ShopBridge.Application
{
    public class SearchDto
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string? Text { get; set; }

        public bool HasPagination()
        {
            return Page.HasValue && Size.HasValue && Size > 0;
        }

        public int Skip()
        {
            if (!Page.HasValue || !Size.HasValue) return 0;
            return Page.Value * Size.Value;
        }
    }
}
