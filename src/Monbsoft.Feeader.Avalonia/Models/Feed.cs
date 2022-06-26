using ReactiveUI;
using System;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Feed : ReactiveObject
    {
        private string _name;

        public Feed(string name, string link)
            : this(name, link, DateTime.UtcNow, null)
        {          
        }
        [JsonConstructor]
        public Feed(string name, string link, DateTime creationDate, string? categoryId)
        {
            Name = name;
            Link = link;
            CreationDate = creationDate;
            CategoryId = categoryId;
        }

        /// <summary>
        /// Gets the creation date
        /// </summary>
        public DateTime CreationDate { get; }
        /// <summary>
        /// Gets or sets the link of the feed
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Gets or sets the name of the feed
        /// </summary>
        public string Name 
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public string? CategoryId { get; set; }

        public override bool Equals(object? obj)
        {
            Feed? other = obj as Feed;  
            
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Name, other.Name) && string.Equals(Link, other.Link);

        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Link.GetHashCode();
        }
        override public string ToString()
        {
            return Name;
        }

    }
}
