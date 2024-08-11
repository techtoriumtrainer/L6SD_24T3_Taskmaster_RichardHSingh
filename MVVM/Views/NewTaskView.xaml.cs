using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;

namespace TaskNoter.MVVM.Views;


public partial class NewTaskView : ContentPage
{
    private readonly DBService TNDatabase;

    public NewTaskView()
	{
		InitializeComponent();
        TNDatabase = new DBService(Constants.DatabasePath);
    }

    private async void AddTaskBTN_Clicked(object sender, EventArgs e)
    {
        var vm = BindingContext as NewTaskViewModel;

        var selectedCategory = vm.Categories.Where(x => x.IsSelected == true).FirstOrDefault(); // after is selected == true is error occurs

        if (selectedCategory != null)
        {
            var task = new MyTask
            {
                TaskName = vm.Task,
                CategoryId = selectedCategory.Id,
                TaskColor = selectedCategory.Color,
                Completed = false
            };

            await TNDatabase.SaveTaskAsync(task);

            var totalTasks = await TNDatabase.GetTaskAsync();
            vm.Tasks.Clear();

            foreach (var eachTask in totalTasks)
            {
                vm.Tasks.Add(eachTask);
            }
           
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Invalid Category Selection", "Please select a category!", "Affirmative");
        }
    }

    private async void AddCategoryBTN_Clicked(object sender, EventArgs e)
    {
        var vm = BindingContext as NewTaskViewModel;

        string category =
            await DisplayPromptAsync("New Category",
            "Implement a new category name",
            maxLength: 20,
            keyboard: Keyboard.Text);

        var random = new Random();

        if (!string.IsNullOrEmpty(category))
        {
            var newCat = new Category
            {
                Id = vm.Categories.Max(x => x.Id) + 1,
                Color = Color.FromRgb(
                    random.Next(0, 255),
                    random.Next(0, 255),
                    random.Next(0, 255)).ToHex(),
                CategoryName = category,
            };

            await TNDatabase.SaveCategoryAsync(newCat);

            var categories = await TNDatabase.GetCategoryAsync();
            vm.Categories.Clear();

            foreach (var cat in categories)
            {
                vm.Categories.Add(cat);
            }

        }
        else
        {
            await DisplayAlert("Invalid Category Selection", "Please implement a new or choose a category!", "Affirmative");
        }
    }

    
    private async void CancelBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainView());
    }
}