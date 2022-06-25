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

        public static Task<Feed> GetFeedDataAsync(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
                var data = SyndicationFeed.Load(reader);
                return Task.FromResult(new Feed(data.Title?.Text ?? "Unknown title", url));
            }
        }
    }
}