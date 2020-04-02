using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.Omdb
{
    public class OmdbSearchResults
    {
        public Search[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
        public bool IsValid
        {
            get { return this.Search.Length > 0 && !string.IsNullOrEmpty(this.Search[0].Title); }
        }

        public string ToJsonLd()
        {
            if (this.IsValid)
            {
                dynamic schema = new JObject();
                schema.Add("@context", "http://schema.org");
                schema.Add("@type", "ItemList");
                schema.itemListElement = new JArray();
                int i = 0;
                foreach (var item in this.Search)
                {
                    dynamic movieschema = new JObject();
                    movieschema.Add("@type", "Movie");
                    movieschema.name = item.Title;
                    if (!string.IsNullOrEmpty(item.Year))
                    {
                        movieschema.name += " (" + item.Year + ")";
                    }
                    movieschema.image = item.Poster;
                    movieschema.dateCreated = DateTime.Today;
                    movieschema.url = $"/Movies/Details/{item.imdbID}";
                    movieschema.director = new JObject { ["@type"] = "Person", ["name"] = "N/A" };
                    i++;
                    schema.itemListElement.Add(new JObject { ["@type"] = "ListItem", ["position"] = i, ["item"] = movieschema });
                }
                return schema.ToString();
            }
            else
            {
                return "";
            }
        }
    }

    public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }


    }


}
