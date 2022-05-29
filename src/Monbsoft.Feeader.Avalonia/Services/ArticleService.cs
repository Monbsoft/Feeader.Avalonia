using Monbsoft.Feeader.Avalonia.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Monbsoft.Feeader.Avalonia.Services
{
    public static class ArticleService
    {
        public static async Task<IEnumerable<Article>> LoadAsync(Feed feed, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var articles = new List<Article>();
                using (var reader = XmlReader.Create(feed.Link))
                {
                    var data = SyndicationFeed.Load(reader);
                    foreach (var item in data.Items)
                    {
                        var article = new Article(item.Id, item.Title.Text, item.PublishDate.DateTime, item.Links.First().Uri)
                            .WithPicture(item.Links.Where(l => l.MediaType != null && l.MediaType.StartsWith("image")).FirstOrDefault()?.Uri)
                            .WithSummary(item.Summary?.Text);

                        articles.Add(article);
                    }
                };
                return articles;
            }, cancellationToken);
        }
    }
}