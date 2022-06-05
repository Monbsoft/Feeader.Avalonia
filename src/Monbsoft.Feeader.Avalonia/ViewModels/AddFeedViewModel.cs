using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using NLog.Fluent;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class AddFeedViewModel : ViewModelBase
    {
        private string? _name;
        private string? _url;

        public AddFeedViewModel()
        {
            this.WhenAnyValue(x => x.Url)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch);

            AddCommand = ReactiveCommand.Create(() =>
            {

                return new Feed(Name, Url);
            });

        }


        public ReactiveCommand<Unit, Feed?> AddCommand { get; }

        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string? Url
        {
            get => _url;
            set => this.RaiseAndSetIfChanged(ref _url, value);
        }

        private async void DoSearch(string url)
        {
            try
            {
                var feed = await FeedService.GetFeedDataAsync(url);
                Name = feed.Name;
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}