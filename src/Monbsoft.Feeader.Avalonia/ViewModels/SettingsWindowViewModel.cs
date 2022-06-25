using Monbsoft.Feeader.Avalonia.Models;
using System.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private readonly Workspace _workspace;
        
        public SettingsWindowViewModel(Workspace workspace)
        {
            Category = new SettingsCategoryViewModel(workspace.Categories);
            Feed = new SettingsFeedViewModel(workspace);

            _workspace = workspace;
        }

        public SettingsCategoryViewModel Category { get; }
        public SettingsFeedViewModel Feed { get; }

        public Workspace GetWorkspace()
        {
            return _workspace;
        }
    }
}