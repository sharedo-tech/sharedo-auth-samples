namespace Sharedo.Api.Services.Models
{
    public class Paging
    {
        public int RowsPerPage { get; set; }
        public int Page { get; set; }

        public Paging(int rpp, int page)
        {
            RowsPerPage = rpp;
            Page = page;
        }
    }
}