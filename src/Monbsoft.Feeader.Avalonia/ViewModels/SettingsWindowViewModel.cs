using Monbsoft.Feeader.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private readonly FeedContext _feedContext;

        public SettingsWindowViewModel(FeedContext context)
        {
            ChangeFeedViewModel = new ChangeFeedViewModel(context);
        }

        public ChangeFeedViewModel ChangeFeedViewModel { get; }
    }
}
