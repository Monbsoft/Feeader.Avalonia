using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Workspace
    {
        public Workspace()
        {
            Categories = new List<Category>();
            Feeds = new List<Feed>();
        }

        [JsonConstructor]
        public Workspace(List<Category> categories, List<Feed> feeds)
        {
            Categories = categories;
            Feeds = feeds;
        }   

        public List<Category> Categories { get; }
        public List<Feed> Feeds { get; }
    }
}
