using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskNoter.MVVM.Models;
using TaskNoter.Service;

namespace TaskNoter.MVVM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DBService _dbService;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        [ObservableProperty]
        private string _taskName;

        [ObservableProperty]
        private string _selectedTask;

        public MainViewModel()
        {
            _dbService = new DBService(Constants.DatabasePath);
            Categories = new ObservableCollection<Category>();
            Tasks = new ObservableCollection<MyTask>();

            Tasks.CollectionChanged += Tasks_CollectionChanged;

            LoadDBData();
        }

        [RelayCommand]
        public async Task AddTaskAsync()
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                var task = new MyTask { TaskName = TaskName };
                await _dbService.SaveTaskAsync(task);
                Tasks.Add(task);
                TaskName = string.Empty;
            }
        }

        [RelayCommand]
        public async Task DeleteTaskAsync(MyTask task)
        {
            if (task != null)
            {
                await _dbService.DeleteTaskAsync(task);
                Tasks.Remove(task);
                UpdateData();
            }
        }

        [RelayCommand]
        public void EditTask(MyTask task)
        {
            if (task != null)
            {
                TaskName = task.TaskName;
                SelectedTask = task.ToString();
            }
        }

        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        [RelayCommand]
        
        public async Task LoadDBData()
        {
            var categories = await _dbService.GetCategoryAsync();
            Categories.Clear();

            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            var tasks = await _dbService.GetTaskAsync();
            Tasks.Clear();

            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }

            UpdateData();
        }


        public void UpdateData()
        {
            foreach (var category in Categories)
            {
                var tasks = Tasks.Where(t => t.CategoryId == category.Id);
                var completed = tasks.Count(t => t.Completed);
                var totalTasks = tasks.Count();

                category.PendingTasks = totalTasks - completed;
                category.Percentage = totalTasks == 0 ? 0 : (float)completed / totalTasks;
            }

            foreach (var task in Tasks)
            {
                var categoryColor = Categories.FirstOrDefault(c => c.Id == task.CategoryId)?.Color;
                task.TaskColor = categoryColor;
            }
        }
    }
}
