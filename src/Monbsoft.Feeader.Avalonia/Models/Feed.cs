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
        public string Name { get; }

    }
}
