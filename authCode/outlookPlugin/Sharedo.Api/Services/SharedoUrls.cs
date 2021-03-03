using System;

namespace Sharedo.Api.Services
{
    public static class SharedoUrls
    {
        public static string Create(string sharedoBaseUrl, string apiUrl)
        {
            var sharedoUri = new Uri(sharedoBaseUrl);
            var apiUri = new Uri(sharedoUri, apiUrl);
            return apiUri.AbsoluteUri;
        }
    }
}