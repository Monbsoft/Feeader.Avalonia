using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Monbsoft.Feeader.Avalonia.ViewModels;
using ReactiveUI;
using System;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class AddFeedWindow : ReactiveWindow<AddFeedViewModel>
    {
        public AddFeedWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => d(ViewModel!.AddCommand.Subscribe(Close)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
