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
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private async Task DoShowDialogAsync(InteractionContext<AddFeedViewModel, Feed> interaction) 
        {
            var dialog = new AddFeedWindow();
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.DataContext = interaction.Input;
            dialog.Height = 200;
            dialog.Width = 355;

            var result = await dialog.ShowDialog<Feed?>(this);
            interaction.SetOutput(result);
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
