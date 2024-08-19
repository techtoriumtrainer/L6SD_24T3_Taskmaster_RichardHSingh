using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using System.Threading.Tasks;

namespace TaskNoter.MVVM.Views
{
    public partial class MainView : ContentPage
    {
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


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainViewModel viewModel)
            {
                await viewModel.LoadDBData();
            }
        }

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
    }
}
