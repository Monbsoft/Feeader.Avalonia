using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class AddFeedView : UserControl
    {
        public AddFeedView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
