using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Workspace
    {
        public Workspace()
        {
            Categories = new ObservableCollection<Category>();
            Feeds = new ObservableCollection<Feed>();
        }
        
        [JsonConstructor]
        public Workspace(ObservableCollection<Category> categories, ObservableCollection<Feed> feeds)
        {
            Categories = categories;
            Feeds = feeds;
        }   

        /// <summary>
        /// Gets the categories
        /// </summary>
        public ObservableCollection<Category> Categories { get; }
        /// <summary>
        /// Gets the feeds
        /// </summary>
        public ObservableCollection<Feed> Feeds { get; }
    }
}
