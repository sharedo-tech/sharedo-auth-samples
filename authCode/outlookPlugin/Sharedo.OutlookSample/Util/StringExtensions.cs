using System;

namespace Sharedo.OutlookSample.Util
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string url)
        {
            Uri parsed;
            if (!Uri.TryCreate(url, UriKind.Absolute, out parsed)) return false;
            return true;
        }
    }
}
