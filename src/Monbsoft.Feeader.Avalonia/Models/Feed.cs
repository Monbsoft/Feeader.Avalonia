using System;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Feed
    {
        public Feed(string name, string link)
            : this(name, link, DateTime.UtcNow)
        {          
        }

        [JsonConstructor]
        public Feed(string name, string link, DateTime creationDate)
        {
            Name = name;
            Link = link;
            CreationDate = creationDate;
        }

        public DateTime CreationDate { get; }
        public string Link { get; set; }
        public string Name { get; set; }

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
