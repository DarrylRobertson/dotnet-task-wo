using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Web.Models.Movies
{
    public class FindCriteria
    {
        public FindCriteria(string id = "", string title = "")
        {
            Id = id;
            Title = title;
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string ToQueryString()
        {
            var queryString = "";
            if (!string.IsNullOrEmpty(this.Id))
            {
                queryString += string.Format("&i={0}", this.Id);
            }
            if (!string.IsNullOrEmpty(this.Title))
            {
                queryString += string.Format("&t={0}", this.Title);
            }
            return queryString;
        }

        public string ToUniqueKey()
        {
            string tag = $"_i-{this.Id}_t-{this.Title}".ToLower();
            return Regex.Replace(tag, @"[^\w\._-]", "");
        }

    }
}
