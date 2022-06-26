using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class SettingsFeedViewModel : ViewModelBase
    {
        private Feed? _selected;
        private SelectedFeedViewModel _selectedFeedViewModel;

        public SettingsFeedViewModel(Workspace workspace)
        {
            Categories = workspace.Categories;
            Feeds = workspace.Feeds;
            SelectedFeed = new SelectedFeedViewModel(workspace);

            this.WhenAnyValue(x => x.Selected)
                .Subscribe(x =>
                {
                    SelectedFeed.Select(x);
                });

            AddCommand = ReactiveCommand.Create(() =>
            {
                Feeds.Add(new Feed("name", "https://feed.com"));
                Debug.WriteLine($"Feed added");
            });
            RemoveCommand = ReactiveCommand.Create(() =>
            {
                if (_selected != null)
                {
                    Feeds.Remove(_selected);
                    Debug.WriteLine($"Feed {_selected?.Name} removed");
                }
            });
            UpCommand = ReactiveCommand.Create(() =>
            {
                if (_selected != null)
                {
                    int index = Feeds.IndexOf(_selected);
                    if(index > 0)
                        Feeds.Move(index, index - 1);
                }                    

            });
            DownCommand = ReactiveCommand.Create(() =>
            {
                if (_selected != null)
                {
                    int index = Feeds.IndexOf(_selected);
                    if (index < Feeds.Count -1)
                        Feeds.Move(index, index + 1);
                }
            });
        }

        /// <summary>
        /// Gets the add command
        /// </summary>
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        /// <summary>
        /// Gets thes categories
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get;
            private set;
        }
        public ReactiveCommand<Unit, Unit> DownCommand { get; }
        /// <summary>
        /// Gets or sets feeds
        /// </summary>
        public ObservableCollection<Feed> Feeds { get; }
        /// <summary>
        /// Gets the remove command
        /// </summary>
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
        /// <summary>
        /// Gets or sets the selected feed
        /// </summary>
        public Feed Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }
        /// <summary>
        /// Gets or sets the selected feed view model
        /// </summary>
        public SelectedFeedViewModel SelectedFeed
        {
            get => _selectedFeedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedFeedViewModel, value);
        }
        public ReactiveCommand<Unit, Unit> UpCommand { get; }
    }
}