using Monbsoft.Feeader.Avalonia.Models;
using System.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel(Workspace workspace)
        {
            EditCategoryViewModel = new EditCategoryViewModel(workspace.Categories);
            EditFeedViewModel = new EditFeedViewModel(workspace.Feeds);
        }

        public EditCategoryViewModel EditCategoryViewModel { get; }
        public EditFeedViewModel EditFeedViewModel { get; }

        public Workspace CreateWorkspace()
        {
            return new Workspace(EditCategoryViewModel.Categories.ToList(), EditFeedViewModel.Feeds.ToList());
        }
    }
}