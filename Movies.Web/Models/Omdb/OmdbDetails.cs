using Newtonsoft.Json.Linq;
using System;

namespace Movies.Web.Models.Omdb
{
    public class OmdbDetails
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public Rating[] Ratings { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }

        public bool IsValid { 
            get { return !string.IsNullOrEmpty(Title); } 
        }

        public string ToJsonLd()
        {
            if (this.IsValid)
            {
                dynamic schema = new JObject();
                schema.Add("@context", "http://schema.org");
                schema.Add("@type", "Movie");
                schema.name = this.Title;
                if (!string.IsNullOrEmpty(this.Year))
                {
                    schema.name += " (" + this.Year + ")";
                }
                schema.actor = new JArray();
                foreach (var actor in this.Actors.Split(','))
                {
                    schema.actor.Add(new JObject { ["@type"] = "Person" , ["name"] = actor.Trim() });
                }
                schema.author = new JArray();
                foreach (var author in this.Writer.Split(','))
                {
                    schema.author.Add(new JObject { ["@type"] = "Person", ["name"] = author.Trim() });
                }
                schema.director = new JArray();
                foreach (var director in this.Director.Split(','))
                {
                    schema.director.Add(new JObject { ["@type"] = "Person", ["name"] = director.Trim() });
                }
                schema.Add("description", this.Plot);
                schema.aggregateRating = new JObject { 
                    ["@type"] = "AggregateRating", 
                    ["bestRating"] = this.imdbRating,
                    ["ratingCount"] = this.imdbVotes.Replace(",",""),
                    ["ratingValue"] = this.imdbRating,
                };
                schema.image = this.Poster;
                schema.dateCreated = DateTime.Today;

                return schema.ToString();
            }
            else
            {
                return "";
            }
        }
    }
    public class Rating
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }



}
