using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();         
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoSettingsAsync)));
        }

        private async Task DoSettingsAsync(InteractionContext<SettingsWindowViewModel, Workspace> interaction)
        {
            var dialog = new SettingsWindow();
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.DataContext = interaction.Input;
            dialog.Height = 480;
            dialog.Width = 850;
            
            await dialog.ShowDialog(this);
            
            interaction.SetOutput(dialog.ViewModel?.GetWorkspace() ?? new Workspace());

        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
