using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private Feed? _selectedFeed;
    private ArticleViewModel? _selectedArticle;

    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<SettingsWindowViewModel, SettingsContext>();

        ShowSettingsCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new SettingsWindowViewModel(new SettingsContext(Feeds));
            var context = await ShowDialog.Handle(store);
            await FeedService.SaveAsync(context.Feeds.ToList());
            LoadFeedsAsync();
        });

        this.WhenAnyValue(x => x.SelectedFeed)
            .Subscribe(LoadArticles);

        FeedService.InitializeCache();
        PictureService.InitializePictureCache();

        RxApp.MainThreadScheduler.Schedule(LoadFeedsAsync);
    }

    public ICommand ShowSettingsCommand { get; }
    public ObservableCollection<ArticleViewModel> Articles { get; } = new();
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

    public Interaction<SettingsWindowViewModel, SettingsContext> ShowDialog { get; }

    public async void LoadFeedsAsync()
    {
        var feeds = await FeedService.LoadAsync();

        SelectedFeed = null;
        Feeds.Clear();

        foreach (var feed in feeds)
        {
            Feeds.Add(feed);
        }
        Trace.TraceInformation("{0} feeds loaded", Feeds.Count);
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