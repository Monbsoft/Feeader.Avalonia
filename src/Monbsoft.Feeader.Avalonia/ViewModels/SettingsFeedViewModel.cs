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
            this.WhenAnyValue(x => x.Selected)
                .Subscribe(x =>
                {
                    if (_selected == null)
                        SelectedFeed = null;
                    else
                        SelectedFeed = new SelectedFeedViewModel(workspace, _selected);
                });
            Categories = workspace.Categories;
            Feeds = workspace.Feeds;

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

        public SelectedFeedViewModel? SelectedFeed
        {
            get => _selectedFeedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedFeedViewModel, value);
        }
    }
}