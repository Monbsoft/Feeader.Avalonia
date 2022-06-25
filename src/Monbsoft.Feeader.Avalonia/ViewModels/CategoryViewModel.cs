using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        internal readonly Category _category;
        private CancellationTokenSource? _cancellationTokenSource;
        private Feed? _selectedFeed;
        private ArticleViewModel? _selectedArticle;

        public CategoryViewModel(Workspace workspace, Category category)
        {
            this.WhenAnyValue(x => x.SelectedFeed)
                .Subscribe(LoadArticles);

            Feeds = new ObservableCollection<Feed>(workspace.Feeds.Where(f => f.CategoryId == category.Id));

            _category = category;
        }

        /// <summary>
        /// Gets the articles
        /// </summary>
        public ObservableCollection<ArticleViewModel> Articles { get; } = new();
        /// <summary>
        /// Gets the feeds
        /// </summary>
        public ObservableCollection<Feed> Feeds { get; }
        /// <summary>
        /// Gets the category name
        /// </summary>
        public string Name => _category.Name;
        /// <summary>
        /// Gets or sets the view model of the selected article
        /// </summary>
        public ArticleViewModel? SelectedArticle
        {
            get => _selectedArticle;
            set => this.RaiseAndSetIfChanged(ref _selectedArticle, value);
        }
        /// <summary>
        /// Gets or sets the selected feed
        /// </summary>
        public Feed? SelectedFeed
        {
            get => _selectedFeed;
            set => this.RaiseAndSetIfChanged(ref _selectedFeed, value);
        }

        private async void LoadArticles(Feed? feed)
        {
            Articles.Clear();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            if (feed != null)
            {
                foreach (var article in await ArticleService.LoadAsync(feed, _cancellationTokenSource.Token))
                {
                    Articles.Add(new ArticleViewModel(article));
                }
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    LoadPictures(_cancellationTokenSource.Token);
                }
            }
            Trace.TraceInformation("{0} articles loaded", Articles.Count);
        }

        private async void LoadPictures(CancellationToken cancellationToken)
        {
            foreach (var article in Articles)
            {
                await article.LoadPictureAsync(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
            Trace.TraceInformation("Pictures loaded");
        }
    }
}