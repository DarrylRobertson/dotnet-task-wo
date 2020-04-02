using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Web.Models.Omdb
{
    public class OmdbAPIHelper
    {
        IMemoryCache _cache;
        const string BASEURL = "http://www.omdbapi.com/";
        public string APIKey { get; set; }

        private string GetAPIUrl()
        {
            if (string.IsNullOrEmpty(this.APIKey))
            {
                throw new Exception("No API Key found. Hint: Add 'APIKey' to your config.");
            }
            return string.Format("{0}?apikey={1}", BASEURL, APIKey);
        }
        public OmdbAPIHelper(string apiKey, IMemoryCache cache)
        {
            APIKey = apiKey;
            _cache = cache;
        }

        public static async Task<JObject> GetJsonAsync(Uri uri)
        {
            // (real-world code shouldn't use HttpClient in a using block; this is just example code)
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(uri).ConfigureAwait(false);
                return JObject.Parse(jsonString);
            }
        }

        public async Task<OmdbSearchResults> Search(Movies.SearchCriteria criteria)
        {
            var requestUrl = this.GetAPIUrl() + criteria.ToQueryString();
            var searchResults = new OmdbSearchResults();
            if (_cache.TryGetValue<OmdbSearchResults>(criteria.ToUniqueKey(), out searchResults) == false)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(requestUrl))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            searchResults = JsonConvert.DeserializeObject<OmdbSearchResults>(apiResponse);
                            _cache.Set<OmdbSearchResults>(criteria.ToUniqueKey(), searchResults);
                        }
                    }
                }
            }
            return searchResults;
        }

        public async Task<OmdbDetails> Find(Movies.FindCriteria criteria)
        {

            var requestUrl = this.GetAPIUrl() + criteria.ToQueryString();

            var details = new OmdbDetails();
            if (_cache.TryGetValue<OmdbDetails>(criteria.ToUniqueKey(), out details) == false)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(requestUrl))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            details = JsonConvert.DeserializeObject<OmdbDetails>(apiResponse);
                            _cache.Set<OmdbDetails>(criteria.ToUniqueKey(), details);
                        }
                    }
                }
            }
            return details;
        }
    }
}
