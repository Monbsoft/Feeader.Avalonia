using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using NLog.Fluent;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class ChangeFeedViewModel : ViewModelBase
    {
        private FeedContext _context;
        private string _name;
        private string _url;
        private Feed? _selectedFeed;
        private FeedState _state;

        public ChangeFeedViewModel(FeedContext context)
        {
            this.WhenAnyValue(x => x.Url)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch);

            this.WhenAnyValue(x => x.SelectedFeed)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(f =>
                {
                    Name = f?.Name;
                    Url = f?.Link;
                    _state = f != null ? FeedState.Change : FeedState.Add;
                });

            Feeds = new ObservableCollection<Feed>(context.Feeds);
            _state = FeedState.Add;
            _context = context;
            
            AddCommand = ReactiveCommand.Create(() =>
            {
                if(_state == FeedState.Change && _selectedFeed != null)
                {
                    _context.Feeds.Remove(_selectedFeed);
                }
                _context.Feeds.Add(new Feed(Name!, Url!));
                return _context;
            });

        }

        /// <summary>
        /// A
        /// </summary>
        public ReactiveCommand<Unit, FeedContext> AddCommand { get; }
        /// <summary>
        /// Gets the feeds
        /// </summary>
        public ObservableCollection<Feed> Feeds { get; }
        /// <summary>
        /// Gets the name of the feed
        /// </summary>
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        /// <summary>
        /// Gets or sets the selected feed
        /// </summary>
        public Feed? SelectedFeed
        {
            get => _selectedFeed;
            set => this.RaiseAndSetIfChanged(ref _selectedFeed, value);
        }
        /// <summary>
        /// Gets the url of the feed
        /// </summary>
        public string? Url
        {
            get => _url;
            set => this.RaiseAndSetIfChanged(ref _url, value);
        }
        
        private async void DoSearch(string? url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var feed = await FeedService.GetFeedDataAsync(url);
                    Name = feed.Name;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        } 
        
        enum FeedState
        {
            Add = 0,
            Change = 1
        }
        
    }
}