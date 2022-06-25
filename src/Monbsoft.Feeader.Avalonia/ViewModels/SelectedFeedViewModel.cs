using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SelectedFeedViewModel : ViewModelBase
    {
        private readonly Feed _feed;
        private Category _category;

        public SelectedFeedViewModel(Workspace workspace, Feed feed)
        {
            this.WhenAnyValue(x => x.SelectedCategory)
                .Subscribe(x => feed.CategoryId = SelectedCategory?.Id);
            _feed = feed;
            _category = workspace.Categories.FirstOrDefault(c => c.Id == feed.CategoryId);
            Categories = workspace.Categories;
        }
        
        public ObservableCollection<Category> Categories
        {
            get;
            private set;
        }

        public Feed Feed
        {
            get => _feed;           
        }
        public Category? SelectedCategory
        {
            get => _category;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }
    }
    
}
