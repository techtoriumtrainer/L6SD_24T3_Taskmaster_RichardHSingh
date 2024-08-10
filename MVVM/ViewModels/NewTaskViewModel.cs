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
    }
}
