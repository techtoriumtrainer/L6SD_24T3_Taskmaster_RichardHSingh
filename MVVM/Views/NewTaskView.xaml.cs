using System.Collections.ObjectModel;
using TaskNoter.MVVM.Models;
using TaskNoter.MVVM.ViewModels;
using TaskNoter.Service;

namespace TaskNoter.MVVM.Views
{
    public partial class NewTaskView : ContentPage
    {
        private readonly NewTaskViewModel _viewModel;

        public NewTaskView()
        {
            InitializeComponent();
            _viewModel = new NewTaskViewModel();
            BindingContext = _viewModel;
        }

        // ================== CODE FOR ALLOWING USER TO CREATE NEW TASK IN NEWTASKVIEW PAGE ======================
        // =======================================================================================================
        private async void AddTaskBTN_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is NewTaskViewModel vm)
            {
                var selectedCategory = vm.Categories.FirstOrDefault(x => x.IsSelected);

                if (selectedCategory != null)
                {
                    var task = new MyTask
                    {
                        TaskName = vm.Task,
                        CategoryId = selectedCategory.Id,
                        TaskColor = selectedCategory.Color,
                        Completed = false
                    };

                    await vm.AddTaskAsync(task);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Invalid Category Selection", "Please select a category!", "OK");
                }
            }
        }

        // ================== CODE FOR ALLOWING USER TO CREATE A NEW CATEGORY IN NEWTASKVIEW ======================
        // ========================================================================================================
        private async void AddCategoryBTN_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is NewTaskViewModel vm)
            {
                string category = await DisplayPromptAsync("New Category", "Enter a new category name", maxLength: 20, keyboard: Keyboard.Text);

                if (!string.IsNullOrEmpty(category))
                {
                    var random = new Random();
                    var newCategory = new Category
                    {
                        CategoryName = category,
                        Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex()
                    };

                    await vm.AddCategoryAsync(newCategory);
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter a valid category name.", "OK");
                }
            }
        }

        // ================== CODE FOR CANCEL BUTTON INCASE USER ACCIDENTLY CLICKED ON NEW TASK BUTTON ======================
        // ==================================================================================================================
        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        // ================== CODE FOR ALLOWING USER TO DELETE THEIR CATEGORY ======================
        // =========================================================================================
        private async void DeletedCategory_Clicked(object sender, EventArgs e)
        {
            if (sender is SwipeItem categoryItem && categoryItem.CommandParameter is Category deleteCategory)
            {
                if (BindingContext is NewTaskViewModel vm)
                {
                    bool delete = await DisplayAlert("Delete Category", $"Are you sure you want to delete the category '{deleteCategory.CategoryName}'?", "Yes", "No");

                    if (delete)
                    {
                        await vm.DeleteCategoryAsync(deleteCategory);
                    }
                }
            }
        }
    }
}
