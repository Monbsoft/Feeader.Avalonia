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

        private async Task DoSettingsAsync(InteractionContext<SettingsWindowViewModel, SettingsContext> interaction)
        {
            var dialog = new SettingsWindow();
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.DataContext = interaction.Input;
            dialog.Height = 450;
            dialog.Width = 850;
            
            await dialog.ShowDialog(this);
            
            interaction.SetOutput(dialog.ViewModel?.CreateContext() ?? new SettingsContext());

        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
