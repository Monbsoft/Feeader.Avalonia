using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private Feed? _selectedFeed;
    private ArticleViewModel? _selectedArticle;


    public MainWindowViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadFeedsAsync);
        this.WhenAnyValue(x => x.SelectedFeed)
            .WhereNotNull()
            .Subscribe(LoadArticles);
    }

    public ObservableCollection<ArticleViewModel> Articles { get; } = new ();

    public ObservableCollection<Feed> Feeds { get; } = new();

    public Feed? SelectedFeed
    {
        get => _selectedFeed;
        set => this.RaiseAndSetIfChanged(ref _selectedFeed, value);
    }

    public ArticleViewModel? SelectedArticle
    {
        get => _selectedArticle;
        set => this.RaiseAndSetIfChanged(ref _selectedArticle, value);
    }

    public async void LoadFeedsAsync()
    {
        FeedService.InitializeCache();
        PictureService.InitializePictureCache();
        var feeds = await FeedService.LoadAsync();      

        foreach(var feed in feeds)
        {
            Feeds.Add(feed);
        }
    }

    private async void LoadArticles(Feed feed)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        foreach(var article in await ArticleService.LoadAsync(feed, _cancellationTokenSource.Token))
        {
            Articles.Add(new ArticleViewModel(article));
        }
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            LoadPictures(_cancellationTokenSource.Token);
        }

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
    }
}
