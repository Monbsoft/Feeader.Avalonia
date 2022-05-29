using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Infrastructure
{
    internal static class Constants
    {
        public const string Cache = "cache";
        public const string FeedFileName = "feeds.json";
        public const string Pictures = "pictures";

        public static string CachePath = Path.Combine(".", Cache);
    }
}
