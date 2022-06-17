using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using NLog.Fluent;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
        public EditFeedViewModel(List<Feed> feeds)
        {
            Feeds = new ObservableCollection<Feed>(feeds);

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
    }
}