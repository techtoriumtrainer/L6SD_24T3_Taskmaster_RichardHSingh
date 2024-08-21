using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskNoter.MVVM.Views
{
    public partial class MainView : ContentPage       
    {
        private readonly DBService _dbService;

        private readonly MainViewModel _mainViewModel;

        public MainView()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            BindingContext = _mainViewModel;
           
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _mainViewModel.UpdateData();
        }

        // ================== CODE FOR ALLOWING USER TO GO TO NEWTASKVIEW TO CREATE NEW TASKS | CATEGORY ======================
        // ====================================================================================================================
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var taskView = new NewTaskView();
            var viewModel = taskView.BindingContext as NewTaskViewModel;

            if (viewModel != null)
            {
                viewModel.Tasks = _mainViewModel.Tasks;
                viewModel.Categories = _mainViewModel.Categories;
            }

            await Navigation.PushModalAsync(taskView);
        }

        // ================== CODE FOR LOADING DB WHEN PAGE IS OPENED BY USER ==============================
        // =================================================================================================
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainViewModel viewModel)
            {
                await viewModel.LoadDBData();
            }
        }

        // ================== CODE FOR ALLOWING USER TO DELETE TASKS IN MAINVIEW PAGE ======================
        // =================================================================================================
        private async void DeletTask_Clicked(object sender, EventArgs e)
        {
            if (sender is SwipeItem taskItem && taskItem.CommandParameter is MyTask deleteTask)
            {
                bool delete = await DisplayAlert("Deleting Task", $"Are you sure you want to delete this task '{deleteTask.TaskName}'?", "Yes", "No");

                if (delete)
                {
                    await _mainViewModel.DeleteTaskAsync(deleteTask);
                }
            }
        }

        // ================== CODE FOR ALLOWING USER TO EDIT TASKS IN MAINVIEW PAGE ======================
        // ===============================================================================================
        private async void EditTask_Clicked(object sender, EventArgs e)
        {
            if (sender is SwipeItem taskItem && taskItem.CommandParameter is MyTask taskToEdit)
            {
                string editedTask = await DisplayPromptAsync("Editing Task", "Enter new task name:", initialValue: taskToEdit.TaskName, maxLength: 20);
                
                  
                if (!string.IsNullOrEmpty(editedTask))
                {
                    taskToEdit.TaskName = editedTask;
                    await _mainViewModel.SaveTaskAsync(taskToEdit);
                    _mainViewModel.UpdateData();
                }
                

            }
        }
    }
}
