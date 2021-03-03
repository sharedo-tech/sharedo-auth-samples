using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Sharedo.Api
{
    public static class DictionaryExtensions
    {
        public static string AsQueryString(this Dictionary<string, string> parameters)
        {
            var kvps = parameters.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value)));
            var query = string.Join("&", kvps.ToArray());

            return query;
        }

        public static string TryGet(this Dictionary<string, string> parameters, string key)
        {
            string value;
            if (parameters.TryGetValue(key, out value)) return value;
            return null;
        }
    }
}