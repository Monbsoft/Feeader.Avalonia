using Monbsoft.Feeader.Avalonia.Models;
using System.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private readonly Workspace _workspace;
        
        public SettingsWindowViewModel(Workspace workspace)
        {
            EditCategoryViewModel = new EditCategoryViewModel(workspace.Categories);

            _workspace = workspace;
        }

        public EditCategoryViewModel EditCategoryViewModel { get; }
        public EditFeedViewModel EditFeedViewModel { get; }

        public Workspace CreateWorkspace()
        {
            return new Workspace();
        }
    }
}