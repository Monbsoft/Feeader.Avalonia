using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class SettingsContext
    {
        public SettingsContext()
        {
            Feeds = new ObservableCollection<Feed>();
        }
        public SettingsContext(IEnumerable<Feed> feeds)
        {
            Feeds = new ObservableCollection<Feed>(feeds);              
        }
        
        public ObservableCollection<Feed> Feeds { get; }
    }
}
