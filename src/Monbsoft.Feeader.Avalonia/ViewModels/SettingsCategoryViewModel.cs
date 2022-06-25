using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

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

}
