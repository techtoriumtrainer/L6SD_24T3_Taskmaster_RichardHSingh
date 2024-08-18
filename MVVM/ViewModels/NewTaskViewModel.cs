using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using CommunityToolkit.Mvvm.ComponentModel;



namespace TaskNoter.MVVM.ViewModels
{
    public partial class NewTaskViewModel :  ObservableObject
    {
        private readonly DBService TNDatabase;

        public string Task {  get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }
        public ObservableCollection<Category> Categories { get; set; }


        public NewTaskViewModel()
        {
            TNDatabase = new DBService(Constants.DatabasePath);

            Tasks = new ObservableCollection<MyTask>();
            Categories = new ObservableCollection<Category>();

            // Asynchronoously loading categories
            CategoryLoadAsync();
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

        public async Task AddTaskAsync(MyTask task)
        {
            await TNDatabase.SaveTaskAsync(task);
            Tasks.Add(task);
        }
        public async Task DeleteTaskAsync(MyTask task)
        {
            await TNDatabase.DeleteTaskAsync(task);
            Tasks.Remove(task);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await TNDatabase.SaveCategoryAsync(category);
            Categories.Add(category);
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

            //if (category == null)
            //{
            //    return;
            //}

            //await TNDatabase.DeleteCategoryAsync(category);

            //Categories.Remove(category);
            //UpdateData();
        }
    }
}
