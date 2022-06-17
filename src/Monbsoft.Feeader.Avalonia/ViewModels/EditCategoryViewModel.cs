using Monbsoft.Feeader.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class EditCategoryViewModel : ViewModelBase
{

    private string _name;
    private Category? _selected;
    private string _url;
    public EditCategoryViewModel(List<Category> categories)
    {
        Categories = new ObservableCollection<Category>(categories);

        AddCommand = ReactiveCommand.Create(() =>
        {
            Categories.Add(new Category("category"));
            Debug.WriteLine($"Category added");
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
