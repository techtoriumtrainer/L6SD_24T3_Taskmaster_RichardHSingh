using Microsoft.VisualBasic.FileIO;
using PropertyChanged;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;
using System.Data;
using SQLite;


namespace TaskNoter.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public partial class MainViewModel : ObservableObject
    {
        // initialise DB again
        private readonly DBService TNDatabase;

            public ObservableCollection<Category> Categories { get; set; }
            public ObservableCollection<MyTask> Tasks { get; set; }


        [ObservableProperty]
        private string taskName;

        [ObservableProperty]
        private string selectedTask;



        public MainViewModel()
        {
            TNDatabase = new DBService(Constants.DatabasePath);

            Categories = new ObservableCollection<Category>();
            Tasks = new ObservableCollection<MyTask>();

            Tasks.CollectionChanged += Tasks_CollectionChanged;

            LoadDBData();
        }


        [RelayCommand]
        public void AddTask()
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                Tasks.Add(new MyTask { TaskName = TaskName });
                TaskName = string.Empty;
            }
        }


        [RelayCommand]
        public async Task DeleteTaskAsync(MyTask task)
        {
            if (task == null)
            {
                return;
            }

            await TNDatabase.DeleteTaskAsync(task);
            Tasks.Remove(task);

            UpdateData();
        }


        [RelayCommand]
        public void EditTask(MyTask task)
        {
            if (task != null)
            {
                TaskName = task.TaskName;
                SelectedTask = task.ToString();
                //AddOrUpdateButtonText = "UpdateTask";
            }
        }


        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }


        [RelayCommand]
        private async Task LoadDBData()
        {
            Categories.Clear();
            var categories = await TNDatabase.GetCategoryAsync();
            

            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            //Categories = new ObservableCollection<Category>(categories);


            Tasks.Clear();
            var tasks = await TNDatabase.GetTaskAsync();
           

            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
            ///Tasks = new ObservableCollection<MyTask>(tasks);            


            UpdateData();
        }


        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = Tasks.Where(t => t.CategoryId == c.Id);
                var completed = tasks.Where(t => t.Completed).Count();
                var totalTasks = tasks.Count();

                c.PendingTasks = totalTasks - completed;
                c.Percentage = totalTasks == 0 ? 0 : (float)completed / totalTasks;
            }
            foreach (var t in Tasks)
            {
                var categoryColor = Categories.FirstOrDefault(c => c.Id == t.CategoryId)?.Color;
                t.TaskColor = categoryColor;
            }
        }     

    }
}


    

