using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Monbsoft.Feeader.Avalonia.Views
{
    public partial class ArticleView : UserControl
    {
        public ArticleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
