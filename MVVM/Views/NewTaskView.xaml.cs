using TaskNoter.MVVM.ViewModels;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TaskNoter.MVVM.Views;


public partial class NewTaskView : ContentPage
{
    private readonly DBService TNDatabase;

    public ObservableCollection<Category> Categories { get; set; }
    public ObservableCollection<MyTask> Tasks { get; set; }
    public string Task {  get; set; }

    public NewTaskView()
    {
        InitializeComponent();
        TNDatabase = new DBService(Constants.DatabasePath);

        BindingContext = new NewTaskViewModel();

        Categories = new ObservableCollection<Category>();
        Tasks = new ObservableCollection<MyTask>();

        CategoryLoadAsync();

    }
  

    private async void AddTaskBTN_Clicked(object sender, EventArgs e)
    {
        var vm = BindingContext as NewTaskViewModel;

        var selectedCategory = vm?.Categories.FirstOrDefault(x => x.IsSelected); //== true).FirstOrDefault(); 

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

    public async Task DeleteTaskAsync(MyTask task)
    {
        await TNDatabase.DeleteTaskAsync(task);
        Tasks.Remove(task);
    }

    private async void AddCategoryBTN_Clicked(object sender, EventArgs e)
    {
        var vm = BindingContext as NewTaskViewModel;

     
        string category = await DisplayPromptAsync("New Category",
                                                   "Implement a new category name",
                                                    maxLength: 20,
                                                    keyboard: Keyboard.Text);

        

        if (!string.IsNullOrEmpty(category))
        {
            var random = new Random();

            int newId = vm.Categories.Any() ? vm.Categories.Max(x => x.Id) + 1 : 1;

            var newCat = new Category
            {
                Id = newId,
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
            await DisplayAlert("Invalid Category Selection", "Please implement a new category or choose an existing one!", "Affirmative");
        }
    }


    private async void CancelBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainView());
    }


    public async Task DeleteCategoryAsync(Category category)
    {
        var deleteTask = Tasks.Where(t => t.CategoryId == category.Id).ToList();
        foreach (var task in deleteTask)
        {
            await DeleteTaskAsync(task);
        }

        await TNDatabase.DeleteCategoryAsync(category);

        Categories.Remove(category);

       
    }

    public async Task CategoryLoadAsync()
    {
        var categories = await TNDatabase.GetCategoryAsync();

        Categories.Clear();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    private async void DeletedCategory_Clicked(object sender, EventArgs e)
    {
        // gets swipped task
        var categoryItem = sender as SwipeItem;

        var deleteCategory = categoryItem?.CommandParameter as Category;


        if (deleteCategory != null)
        {
            var vm = BindingContext as NewTaskView;

            if (vm != null) // && category.Category.Contains(deleteCategory)
            {
                bool delete = await DisplayAlert("Deleting Task", $"Are you sure you want to delete this task '{deleteCategory.CategoryName}'?", "Yes", "No");

                if (delete)
                {
                    await vm.DeleteCategoryAsync(deleteCategory);

                    await vm.CategoryLoadAsync();
                }
            }
            else
            {
                await DisplayAlert("Warning", "ViewModel is not set CORRECTLY.", "OK");
            }

        }
    }

}