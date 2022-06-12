using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.ViewModels;
using ReactiveUI;
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

        private async Task DoSettingsAsync(InteractionContext<SettingsWindowViewModel, MainWindowViewModel> interaction)
        {
            var dialog = new SettingsWindow();
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.DataContext = interaction.Input;
            dialog.Height = 450;
            dialog.Width = 850;

            var result = await dialog.ShowDialog<MainWindowViewModel>(this);
            interaction.SetOutput(result);

        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
