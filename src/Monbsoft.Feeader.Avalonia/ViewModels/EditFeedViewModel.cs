using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using NLog.Fluent;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class EditFeedViewModel : ViewModelBase
    {
        private string _name;
        private Feed? _selected;
        private string _url;
        public EditFeedViewModel(SettingsContext context)
        {
            this.WhenAnyValue(x => x.Url)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch);

            Feeds = new ObservableCollection<Feed>(context.Feeds);

            AddCommand = ReactiveCommand.Create(() =>
            {
                Feeds.Add(new Feed("name", "https://feed.com"));
                Debug.WriteLine($"Feed added");
            });
            RemoveCommand = ReactiveCommand.Create(() =>
            {
                if(_selected != null)
                {
                    Feeds.Remove(_selected);
                    Debug.WriteLine($"Feed {_selected?.Name} removed");
                }
            });

        }

        /// <summary>
        /// Gets the add command
        /// </summary>
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ObservableCollection<Feed> Feeds { get; }
        /// <summary>
        /// Gets the name of the feed
        /// </summary>
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
        /// <summary>
        /// Gets or sets the selected feed
        /// </summary>
        public Feed SelectedFeed 
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
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
    }
}