using Monbsoft.Feeader.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Monbsoft.Feeader.Avalonia.Services
{
    public static class FeedService
    {
        private const string _localFileName = "feeds.json";
        private static string _localPath = Path.Combine(".", "cache");

        public static string LocalFullFileName
            => Path.Combine(_localPath, _localFileName);

        public static async Task<List<Feed>> LoadAsync()
        {
            List<Feed>? feeds = null;

            if (!File.Exists(LocalFullFileName))
                return new List<Feed>();

            try
            {
                using var stream = File.OpenRead(LocalFullFileName);
                feeds = await JsonSerializer.DeserializeAsync<List<Feed>>(stream);

            }
            catch(NotSupportedException)
            {                
            }
                return feeds ?? new List<Feed>();
        }

        public static Task<Feed> GetFeedDataAsync(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
                var data = SyndicationFeed.Load(reader);
                return Task.FromResult(new Feed(data.Title?.Text, url));
            }
        }

        public static Task SaveAsync(List<Feed> feeds)
        {
            if (!Directory.Exists(_localPath))
                Directory.CreateDirectory(_localPath);

            File.Delete(LocalFullFileName);
            using Stream stream = File.OpenWrite(LocalFullFileName);
            return JsonSerializer.SerializeAsync(stream, feeds);
        }
    }
}