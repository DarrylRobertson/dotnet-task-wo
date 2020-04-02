using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Web.Models.Movies
{
    public class SearchCriteria
    {
        public SearchCriteria(string keyword = "", string year = "", int pageNo = 0)
        {
            Keyword = keyword;
            Year = year;
            PageNo = pageNo;
        }
        public string Keyword { get; set; }
        public string Year { get; set; }
        public int PageNo { get; set; }
        public string ToQueryString()
        {
            var queryString = "";
            if (!string.IsNullOrEmpty(this.Keyword))
            {
                queryString += string.Format("&s={0}", this.Keyword);
            }
            if (!string.IsNullOrEmpty(this.Year))
            {
                queryString += string.Format("&y={0}", this.Year);
            }
            if (this.PageNo > 0)
            {
                queryString += string.Format("&page={0}", this.PageNo);
            }
            return queryString;
        }

        public string ToUniqueKey()
        {
            string tag = $"_k-{this.Keyword}_y-{this.Year}_p-{this.PageNo}".ToLower();
            return Regex.Replace(tag, @"[^\w\._-]", "");
        }
    }
}
