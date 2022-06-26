using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SelectedFeedViewModel : ViewModelBase
    {
        private Feed _feed;
        private Category _category;

        public SelectedFeedViewModel(Workspace workspace)
        {
            Categories = workspace.Categories;

            this.WhenAnyValue(x => x.Feed)
                .Subscribe(x =>
                {
                    if (Feed != null)
                        SelectedCategory = Categories.FirstOrDefault(x => x.Id == Feed.CategoryId);
                    else
                        SelectedCategory = null;
                });
            this.WhenAnyValue(x => x.SelectedCategory)
                .Subscribe(x => 
                {
                    if (Feed != null)
                        Feed.CategoryId = SelectedCategory?.Id;
                });
        }

        /// <summary>
        /// Gets the categories
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the feed
        /// </summary>
        public Feed Feed
        {
            get => _feed;
            private set => this.RaiseAndSetIfChanged(ref _feed, value);
        }
        /// <summary>
        /// Gets or sets the selected category.
        /// </summary>
        public Category? SelectedCategory
        {
            get => _category;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }

        public void Select(Feed feed)
        {
            Feed = feed;                 
        }
    }
}