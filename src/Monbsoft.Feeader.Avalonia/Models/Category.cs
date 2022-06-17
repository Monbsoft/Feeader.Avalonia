using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Category
    {
        public Category(string name)
            : this(Guid.NewGuid().ToString(), name, new List<Feed>())
        {            
        }
        
        [JsonConstructor]
        public Category(string id, string name, List<Feed> feeds)
        {
            Id = id;
            Name = name;
            Feeds = feeds;
        }

        public List<Feed> Feeds { get;  }
        public string Id { get; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
