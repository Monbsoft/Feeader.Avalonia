using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class EditCategoryViewModel : ViewModelBase
{
    private Category? _selected;
    private string? _name;

    public EditCategoryViewModel(List<Category> categories)        
    {
        this.WhenAnyValue(x => x.Name)            
            .Subscribe(x =>
            {
                if (_selected != null && x != null)
                    _selected.Name = x;
                
            });


        Categories = new ObservableCollection<Category>(categories);
        
        AddCommand = ReactiveCommand.Create(() =>
        {
            Categories.Add(new Category("category"));
            Debug.WriteLine("Category added");
        });
        RemoveCommand = ReactiveCommand.Create(() =>
        {
            if (_selected != null)
            {
                Categories.Remove(_selected);
                Debug.WriteLine($"Feed {_selected?.Name} removed");
            }
        });
    }

    /// <summary>
    /// Gets the add command
    /// </summary>
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ObservableCollection<Category> Categories { get; }
    /// <summary>
    /// Gets the name of the category
    /// </summary>
    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
    /// <summary>
    /// Gets or sets the selected category
    /// </summary>
    public Category SelectedCategory
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }

}
