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
        public async void DeleteTask(MyTask task)
        {
            if (task != null)
            {
                await TNDatabase.DeleteTaskAsync(task);
                Tasks.Remove(task);
                LoadDBData();
            }
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
        private async void LoadDBData()
        {
            var categories = await TNDatabase.GetCategoryAsync();
            Categories.Clear();

            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            //Categories = new ObservableCollection<Category>(categories);


            var tasks = await TNDatabase.GetTaskAsync();
            Tasks.Clear();

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
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var notCompleted = from t in tasks
                                   where t.Completed == false
                                   select t;


                c.PendingTasks = notCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var categoryColor =
                    (from c in Categories
                     where c.Id == t.CategoryId
                     select c.Color).FirstOrDefault();
                t.TaskColor = categoryColor;
            }
        }

        public async Task DeleteTaskAsync(MyTask task)
        {
            if (task == null)
            {
                return;
            }

            await TNDatabase.DeleteTaskAsync(task);
            Tasks.Remove(task);

            UpdateData();

            return;

        }
        //public void UpdateData()
        //{
        //    foreach (var c in Categories)
        //    {
        //        var tasks = Tasks.Where(t => t.CategoryId == c.Id);
        //        var completed = tasks.Where(t => t.Completed).Count();
        //        var totalTasks = tasks.Count();

        //        c.PendingTasks = totalTasks - completed;
        //        c.Percentage = totalTasks == 0 ? 0 : (float)completed / totalTasks;
        //    }
        //    foreach (var t in Tasks)
        //    {
        //        var catColor = Categories.FirstOrDefault(c => c.Id == t.CategoryId)?.Color;
        //        t.TaskColor = catColor;
        //    }
        //}

        //public async Task DeleteTaskAsync(MyTask task)
        //{
        //    if (task == null) return;
        //    await TNDatabase.DeleteTaskAsync(task);
        //    Tasks.Remove(task);
        //    UpdateData();

        //}
    }
}


    

