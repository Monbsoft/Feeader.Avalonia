using Monbsoft.Feeader.Avalonia.Infrastructure;
using Monbsoft.Feeader.Avalonia.Models;
using NLog.Fluent;
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
        private static string s_cacheFeedFilePath = Path.Combine(Constants.CachePath, Constants.FeedFileName);

        public static void InitializeCache()
        {
            if (!Directory.Exists(Constants.Cache))
                Directory.CreateDirectory(Constants.Cache);
        }

        public static async Task<List<Feed>> LoadAsync()
        {
            List<Feed>? feeds = null;

            try
            {
                if (File.Exists(s_cacheFeedFilePath))
                {
                    using (var stream = File.OpenRead(s_cacheFeedFilePath))
                    {
                        feeds = await JsonSerializer.DeserializeAsync<List<Feed>>(stream);
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                Log.Error(ex.Message);
                feeds = null;
            }
            return feeds ?? new List<Feed>();
        }

        public static Task<Feed> GetFeedDataAsync(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
                var data = SyndicationFeed.Load(reader);
                return Task.FromResult(new Feed(data.Title?.Text ?? "Unknown title", url));
            }
        }

        public static async Task SaveAsync(List<Feed> feeds)
        {
            try
            {
                if (File.Exists(s_cacheFeedFilePath))
                    File.Delete(s_cacheFeedFilePath);

                using (var stream = File.OpenWrite(s_cacheFeedFilePath))
                {
                    await JsonSerializer.SerializeAsync(stream, feeds);
                }
            }
            catch (NotSupportedException ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}