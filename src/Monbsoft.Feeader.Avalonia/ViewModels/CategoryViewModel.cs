using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        internal readonly Category _category;
        private CancellationTokenSource? _cancellationTokenSource;
        private Feed? _selectedFeed;
        private ArticleViewModel? _selectedArticle;

        public CategoryViewModel(Category category)
        {
            this.WhenAnyValue(x => x.SelectedFeed)
                .Subscribe(LoadArticles);

            _category = category;
        }

        public ObservableCollection<ArticleViewModel> Articles { get; } = new();
        public ObservableCollection<Feed> Feeds { get; set; }
        public string Id => _category.Id;
        public string Name => _category.Name;
        public ArticleViewModel? SelectedArticle
        {
            get => _selectedArticle;
            set => this.RaiseAndSetIfChanged(ref _selectedArticle, value);
        }
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