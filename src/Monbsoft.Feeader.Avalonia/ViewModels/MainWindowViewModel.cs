using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private Feed? _selectedFeed;
    private ArticleViewModel? _selectedArticle;

    public MainWindowViewModel()
    {

        ShowDialog = new Interaction<AddFeedViewModel, Feed?>();

        AddFeedCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new AddFeedViewModel();
            var feed = await ShowDialog.Handle(store);
            AddFeed(feed);
        });

        this.WhenAnyValue(x => x.SelectedFeed)
            .WhereNotNull()
            .Subscribe(LoadArticles);

        RxApp.MainThreadScheduler.Schedule(LoadFeedsAsync);
    }

    public ICommand AddFeedCommand { get; }
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
    public Interaction<AddFeedViewModel, Feed?> ShowDialog { get; }

    public async void LoadFeedsAsync()
    {
        FeedService.InitializeCache();
        PictureService.InitializePictureCache();
        var feeds = await FeedService.LoadAsync();      

        foreach(var feed in feeds)
        {
            Feeds.Add(feed);
        }
        Trace.TraceInformation("{0} feeds loaded", Feeds.Count);
    }
    private async void AddFeed(Feed? feed)
    {
        if (feed == null)
            return;
        Feeds.Add(feed);
        await FeedService.SaveAsync(Feeds.ToList());
    }
    private async void LoadArticles(Feed feed)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        Articles.Clear();
        foreach(var article in await ArticleService.LoadAsync(feed, _cancellationTokenSource.Token))
        {
            Articles.Add(new ArticleViewModel(article));
        }
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            LoadPictures(_cancellationTokenSource.Token);
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
