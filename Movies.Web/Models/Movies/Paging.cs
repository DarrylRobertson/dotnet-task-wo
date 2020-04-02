using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.Movies
{
    public class Paging
    {
        public Paging()
        {
        }
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public string BaseUrl { get; set; }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        public string Status
        {
            get
            {
                string status = "";
                if (Count == 0)
                {
                    status = "No items found.";
                }
                else if (Count == 1)
                {
                    status = "Showing 1 item.";
                }
                else if (Count <= PageSize)
                {
                    status = $"Showing {Count} items.";
                }
                else
                {
                    status = $"{Count:n0} items found. Showing page {CurrentPage} of {TotalPages}.";
                }
                return status;
            }

        }
    }
}
