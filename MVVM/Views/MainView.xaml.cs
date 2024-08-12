using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;

namespace TaskNoter.MVVM.Views;

public partial class MainView : ContentPage
{
	private MainViewModel mainViewModel = new MainViewModel();

	public MainView()
	{
		InitializeComponent();

		BindingContext = mainViewModel;
	}

    private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		mainViewModel.UpdateData();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView
        {
            BindingContext = new NewTaskViewModel
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

        var deleteTask = taskItem?.CommandParameter as MyTask;


        if (deleteTask != null)
        {
            var task = BindingContext as MainViewModel;

            if (task != null && task.Tasks.Contains(deleteTask))
            {
             bool delete = await DisplayAlert("Deleting Task", $"Are you sure you want to delete this task '{task.TaskName}'?", "Yes", "No");

                if (delete)
                {
                    await task.DeleteTaskAsync(deleteTask);
                }
            }

        }
    }
}