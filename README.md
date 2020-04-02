Whistleout .NET Engineer Technical Task
=======================================
The purpose of this task is to assess your technical skills and get an idea on how you approach a problem. We know that you might not have a lot of time to devote to this task so please work on the Core Task Details and the rest of them if you have time.

# Core Task Details:
Your task is to create the following:
* A movie search results page to search for movies by title using the omdbapi. Example  http://www.omdbapi.com/?apikey=[yourkey]&s=Avengers. Ensure any criteria used to search movies is in the query string of the movie search results page.
* A movie details page displaying more details about a movie using the omdbapi. Example http://www.omdbapi.com/?apikey=[yourkey]&i=tt4154756
* Mark up the movie search results page with movie carousel structured data for the current displayed movies. Make sure the structured data is in JSON-LD format. More guidelines are here https://developers.google.com/search/docs/data-types/movie#summary
* Mark up the movie details page with movie structured data for the displayed movie details. Make sure the structured data is in JSON-LD format. More guidelines are here https://developers.google.com/search/docs/data-types/movie#movie

## Notes:
Structured data in JSON-LD should be generated in the html head section. More details about them are available here https://developers.google.com/search/docs/guides/intro-structured-data. You can validate your structured data using the tool https://search.google.com/structured-data/testing-tool

# Getting Started:
We have provided a boilerplate code that displays movies from a hard coded set of data. Feel free to use this and build upon it. You are NOT REQUIRED to use this and feel free to start a solution from scratch. Make sure this is done in .NET.

# Bonus Task:
* Add a control to search movies by an year range in addition to the title in search results page. Make sure this is part of the query string as well.
* Implement paging on the movie search results page

# Super Bonus Task:
Use a React component in movie details page to implement an autocomplete to quickly navigate to a different movie details. The autocomplete will pull details using the omdb api http://www.omdbapi.com/?apikey=[yourkey]&s=[text]

# Dependencies:
This task requires you to register for an API key from http://www.omdbapi.com/apikey.aspx to use their API. Make sure the API key can be configured from web.config or some configuration. DO NOT ADD YOUR API KEY TO SOURCE CONTROL. We will use our key to test your solution.

# Evaluation Criteria:
Evaluation is based on clean coding skills, design & problem solving skills and easy maintenance of solution.

# Submission:
Document the solution with design choices you made and any assumptions in SUBMISSION.md. Also include any instructions on any other setup required to run the solution.
After you have finished the solution please upload to a public Github repo and share the link with jobsau@whistleout.com
