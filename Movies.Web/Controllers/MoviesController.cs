using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movies.Web.Models.Movies;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Movies.Web.Models.Omdb;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        IConfiguration _config;
        IMemoryCache _cache;
        public MoviesController(IConfiguration configuration, IMemoryCache cache)
        {
            _config = configuration;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var apiKey = _config.GetValue<string>("APIKey");
            var apiHelper = new OmdbAPIHelper(apiKey, _cache);
            ViewBag.SearchCriteria = new SearchCriteria();

            var searchCriteria = new SearchCriteria { Keyword = "Star+Trek" };

            var task = Task.Run(() => apiHelper.Search(searchCriteria));
            task.Wait();
            var response = task.Result;

            if (response.Response != "True")
            {
                ViewBag.Feedback = "Feature Movies Unavailable";
                return View();
            }
            var searchResults = new SearchResults { Results = new List<SearchResult>() };
            // Create a SearchResults object, initialise the Results list, load with 4 items to Feature
            foreach (var movie in response.Search.Take(4))
            {
                searchResults.Results.Add(new SearchResult { Id = movie.imdbID, Title = movie.Title, Year = movie.Year, ImageUrl = movie.Poster });
            }
            ViewBag.Feedback = "Featured Movies";
            return View(searchResults);
        }

        public IActionResult Search()
        {
            if (Request.Query.Keys.Count == 0)
            {
                ViewBag.Feedback = "";
                return View();
            }
            var apiKey = _config.GetValue<string>("APIKey");
            var apiHelper = new OmdbAPIHelper(apiKey, _cache);
            var searchCriteria = new SearchCriteria();
            searchCriteria.PageNo = 1;
            searchCriteria.Keyword = Request.Query["keyword"];
            searchCriteria.Year = Request.Query["year"];
            if (!string.IsNullOrEmpty(Request.Query["pageno"]))
            {
                var pageNoValue = string.Format("{0}", Request.Query["pageno"]);
                searchCriteria.PageNo = Convert.ToInt32(pageNoValue ?? "0");
            }
            ViewBag.SearchCriteria = searchCriteria;

            var task = Task.Run(() => apiHelper.Search(searchCriteria));
            task.Wait();
            var response = task.Result;

            if (response.Response != "True")
            {
                ViewBag.Feedback = "No Movies were found.";
                return View();
            }
            var paging = new Paging();
            paging.Count = Convert.ToInt32(response.totalResults ?? "0");
            paging.CurrentPage = searchCriteria.PageNo;
            paging.BaseUrl = Request.Path + string.Format("?keyword={0}&year={1}", searchCriteria.Keyword, searchCriteria.Year);
            ViewBag.Paging = paging;

            var searchResults = new SearchResults { Results = new List<SearchResult>() };
            foreach (var movie in response.Search)
            {
                searchResults.Results.Add(new SearchResult { Id = movie.imdbID, Title = movie.Title, Year = movie.Year, ImageUrl = movie.Poster });
            }
            ViewBag.JsonLd = response.ToJsonLd();
            return View(searchResults);
        }

        public IActionResult Details(string id, string title = "")
        {
            var apiKey = _config.GetValue<string>("APIKey");
            ViewBag.APIKey = apiKey;
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var apiHelper = new OmdbAPIHelper(apiKey, _cache);
                var findCriteria = new FindCriteria { Id = id };

                var task = Task.Run(() => apiHelper.Find(findCriteria));
                task.Wait();

                var movieDetail = new OmdbDetails();
                var response = task.Result;

                if (response.IsValid == false)
                {
                    return View();
                }
                ViewBag.JsonLd = response.ToJsonLd();
                return View(response);

            }
        }
    }
}
