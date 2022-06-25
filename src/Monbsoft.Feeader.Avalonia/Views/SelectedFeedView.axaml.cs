using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class SelectedFeedView : UserControl
    {
        public SelectedFeedView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
