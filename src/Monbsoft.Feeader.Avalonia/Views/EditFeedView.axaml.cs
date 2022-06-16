using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class EditFeedView : UserControl
    {
        public EditFeedView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
