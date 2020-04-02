# Submission:
Document the solution with design choices you made and any assumptions in SUBMISSION.md. Also include any instructions on any other setup required to run the solution.
After you have finished the solution please upload to a public Github repo and share the link with jobsau@whistleout.com

# Approach:
My approach is to keep existing code in place as much as possible, to not install new packages or libraries and to attempt to follow the existing naming conventions.
I added some caching to reduce burden on the API.

# Installation:
Create a configuration key in appsettings.Development.json called "APIKey".
Your Development config should look like this:
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "APIKey": "87de8ac3"
}

