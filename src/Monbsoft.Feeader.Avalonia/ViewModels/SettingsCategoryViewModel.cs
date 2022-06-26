using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class SettingsCategoryViewModel : ViewModelBase
{
    private Category? _selected;

    public SettingsCategoryViewModel(ObservableCollection<Category> categories)
    {
        Categories = categories;

        AddCommand = ReactiveCommand.Create(() =>
        {
            var category = new Category("category");
            Categories.Add(category);

            Debug.WriteLine("Category added");
        });
        RemoveCommand = ReactiveCommand.Create(() =>
        {
            if (_selected != null)
            {
                Categories.Remove(_selected);
                Debug.WriteLine($"Category {_selected?.Name} removed");
            }
        });
        UpCommand = ReactiveCommand.Create(() =>
        {
            if (_selected != null)
            {
                int index = Categories.IndexOf(_selected);
                if (index > 0)
                    Categories.Move(index, index - 1);                                   
            }
        });
        DownCommand = ReactiveCommand.Create(() =>
        {
            if (_selected != null)
            {
                int index = Categories.IndexOf((_selected));
                if (index < Categories.Count - 1)
                    Categories.Move(index, index + 1);
            }
        });
    }

    /// <summary>
    /// Gets the add command
    /// </summary>
    public ReactiveCommand<Unit, Unit> AddCommand { get; }

    /// <summary>
    /// Gets the categories
    /// </summary>
    public ObservableCollection<Category> Categories { get; }

    /// <summary>
    /// Gets the down command
    /// </summary>
    public ReactiveCommand<Unit, Unit> DownCommand { get; }

    /// <summary>
    /// Gets the remove command
    /// </summary>
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    /// <summary>
    /// Gets or sets the selected category
    /// </summary>
    public Category Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }

    /// <summary>
    /// Gets the up command
    /// </summary>
    public ReactiveCommand<Unit, Unit> UpCommand { get; }
}