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

        public SettingsWindowViewModel(SettingsContext context)
        {
            EditFeedViewModel = new EditFeedViewModel(context);
        }

        public EditFeedViewModel EditFeedViewModel { get; }

        public SettingsContext CreateContext()
        {
            return new SettingsContext(EditFeedViewModel.Feeds);
        }
    }
}
