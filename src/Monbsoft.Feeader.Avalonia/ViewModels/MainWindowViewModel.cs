using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace Monbsoft.Feeader.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private Workspace _workspace;
    

    public MainWindowViewModel()
    {
        Categories = new ObservableCollection<CategoryViewModel>();
        ShowDialog = new Interaction<SettingsWindowViewModel, Workspace>();

        SettingsCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new SettingsWindowViewModel(_workspace);
            var result = await ShowDialog.Handle(store);
            await WorkspaceService.SaveAsync(result);
            LoadWorkspaceAsync();
        });        

        PictureService.InitializePictureCache();

        RxApp.MainThreadScheduler.Schedule(LoadWorkspaceAsync);
    }

    public ICommand SettingsCommand { get; }

    public ObservableCollection<CategoryViewModel> Categories { get; } = new();

    
    public Interaction<SettingsWindowViewModel, Workspace> ShowDialog { get; }

    public async void LoadWorkspaceAsync()
    {
        Debug.WriteLine("Loading workspace...");
        _workspace = await WorkspaceService.LoadAsync();
        Categories.Clear();
        Categories.Add(new CategoryViewModel(_workspace, new Category("test")));
        foreach(var category in _workspace.Categories)
        {
            Categories.Add(new CategoryViewModel(_workspace, category));
        }
        Trace.TraceInformation("{0} feeds loaded", _workspace.Feeds.Count());
    }


}