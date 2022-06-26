using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SelectedFeedViewModel : ViewModelBase
    {
        private readonly Feed _feed;
        private Category _category;

        public SelectedFeedViewModel(Workspace workspace, Feed feed)
        {
            Categories = workspace.Categories;
            _feed = feed;

            this.WhenAnyValue(x => x.Feed)
                .Subscribe(x =>
                {
                    if (Feed != null)
                        SelectedCategory = Categories.FirstOrDefault(x => x.Id == Feed.CategoryId);
                    else
                        SelectedCategory = null;
                });
            this.WhenAnyValue(x => x.SelectedCategory)
                .Subscribe(x => feed.CategoryId = SelectedCategory?.Id);
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