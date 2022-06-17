using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class EditCategoryView : UserControl
    {
        public EditCategoryView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
