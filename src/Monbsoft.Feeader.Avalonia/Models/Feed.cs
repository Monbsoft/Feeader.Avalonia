using System;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Feed
    {
        public Feed(string name, string link)
            : this(name, link, DateTime.UtcNow)
        {          
        }

        public Feed(string name, string link, DateTime date)
        {
            Name = name;
            Link = link;
            CreationDate = date;
        }

        public DateTime CreationDate { get; }
        public string Link { get; set; }
        public string Name { get; }

    }
}
