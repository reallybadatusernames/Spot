using Spot.Domain.Entities;
using System;

namespace Spot.Infrastructure.Extensions
{
    public static class SiteExtensions
    {
        public static Uri SiteUri(this Site site)
        {
            return site.UsesHttps
                ? new Uri(string.Format("https://{0}", site.Name))
                : new Uri(string.Format("http://{0}", site.Name));
        }

        public static Uri SiteUri(this string site, bool UsesHttps = false)
        {
            return UsesHttps
                ? new Uri(string.Format("https://{0}", site))
                : new Uri(string.Format("http://{0}", site));
        }

        public static Uri SiteUri(this string site, string path = "", bool UsesHttps = false)
        {
            return string.IsNullOrEmpty(path) ?
                UsesHttps
                    ? new Uri(string.Format("https://{0}", site))
                    : new Uri(string.Format("http://{0}", site)) :
                UsesHttps
                    ? new Uri(string.Format(path.StartsWith("/") ? "https://{0}{1}" : "https://{0}/{1}", site, path))
                    : new Uri(string.Format(path.StartsWith("/") ? "http://{0}{1}" : "http://{0}/{1}", site, path));
        }
    }
}
