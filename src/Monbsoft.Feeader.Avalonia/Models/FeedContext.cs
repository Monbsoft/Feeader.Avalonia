using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class FeedContext
    {
        public FeedContext(IEnumerable<Feed> feeds)
        {
            Feeds = feeds.ToList();
        }
        
        public List<Feed> Feeds { get; }
    }
}
