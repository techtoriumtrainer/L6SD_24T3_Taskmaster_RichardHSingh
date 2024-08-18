using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using System.Threading.Tasks;

namespace TaskNoter.MVVM.Views;
public partial class MainView : ContentPage
{
    private MainViewModel mainViewModel;
    private NewTaskViewModel newTaskModel;
    private readonly DBService TNDatabase;

    public MainView()
	{
		InitializeComponent();
        mainViewModel = new MainViewModel();
        BindingContext = mainViewModel;

        newTaskModel = new NewTaskViewModel();
        BindingContext = newTaskModel;

    }

    
    private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		mainViewModel.UpdateData();
    }

    // ====================================================
    // ======= CODE FOR ADDING TASKS AND CATEGORIES =======
    // ====================================================
    private async void Button_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView //mainViewModel.TNDatabase, mainViewModel.Tasks, mainViewModel.Categories
        {
            BindingContext = new NewTaskView
            {
                Tasks = mainViewModel.Tasks,
                Categories = mainViewModel.Categories,
            }
        };

        Navigation.PushModalAsync(taskView);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.UpdateData();
        }
    }

    private async void DeletTask_Clicked(object sender, EventArgs e)
    {
        // gets swipped task
        var taskItem = sender as SwipeItem;

        if (taskItem != null)
        {
            //var task = BindingContext as MainViewModel;
            var deleteTask = taskItem?.CommandParameter as MyTask;

            if (deleteTask != null ) ///&& task.Tasks.Contains(deleteTask)
            {

             bool delete = await DisplayAlert("Deleting Task", $"Are you sure you want to delete this task '{deleteTask.TaskName}'?", "Yes", "No");

                if (delete)
                {
                    await mainViewModel.DeleteTaskAsync(deleteTask);
                }
            }

        }
    }
}