using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskNoter.MVVM.Models;

namespace TaskNoter.Service
{
    public class DBService
    {
        SQLiteAsyncConnection TNDatabase;

        public DBService(string dbPath)
        {
            // Initialize DB and create tables
            TNDatabase = new SQLiteAsyncConnection(dbPath);
            TNDatabase.CreateTableAsync<MyTask>().Wait();
            TNDatabase.CreateTableAsync<Category>().Wait();
        }

        // ================== INITIALISE DB IF NOT EXISTING =====================
        // ======================================================================
        async Task Init()
        {
            if (TNDatabase != null)
            {
                return;
            }

            SQLitePCL.Batteries_V2.Init();
            TNDatabase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            await TNDatabase.CreateTableAsync<MyTask>();
            await TNDatabase.CreateTableAsync<Category>();
        }

        // ================== TABLE CREATION ======================
        // ========================================================
        public async Task<List<MyTask>> GetTaskAsync()
        {
            await Init();
            return await TNDatabase.Table<MyTask>().ToListAsync();
        }

        public async Task<List<Category>> GetCategoryAsync()
        {
            await Init();
            return await TNDatabase.Table<Category>().ToListAsync();
        }


        // ================== CRUD FOR TASKS ======================
        // ========================================================

        public async Task<int> SaveTaskAsync(MyTask task)
        {
            await Init();
            if (task.Id != 0)
            {
                return await TNDatabase.UpdateAsync(task);
            }
            else
            {
                return await TNDatabase.InsertAsync(task);
            }
        }

        public async Task<int> DeleteTaskAsync(MyTask task)
        {
            await Init();
            return await TNDatabase.DeleteAsync(task);
        }




        // ================== CRUD FOR CATEGORY ======================
        // ===========================================================
        public async Task<int> SaveCategoryAsync(Category category)
        {
            await Init();
            if (category.Id != 0)
            {
                return await TNDatabase.UpdateAsync(category);
            }
            else
            {
                return await TNDatabase.InsertAsync(category);
            }
        }

        public async Task<int> DeleteCategoryAsync(Category category)
        {
            await Init();
            return await TNDatabase.DeleteAsync(category);
        }


        // ================== FUNCTION FOR STATICALLY FILLING IN CATEGORY AND TASKS ======================
        // ===============================================================================================
        public async Task FillData()
        {
            var categories = new List<Category>
            {
                new Category { CategoryName = "Watch Anime", Color = "#800080" },
                new Category { CategoryName = "Daily Chores", Color = "#008000" },
                new Category { CategoryName = "Outside Tasks", Color = "#0000FF" }
            };

            var tasks = new List<MyTask>
            {
               new MyTask { TaskName = "Create a watch list", Completed = false, CategoryId = 1, TaskColor = "#800080" },
               new MyTask { TaskName = "Finish Fate Series", Completed = true, CategoryId = 1, TaskColor = "#800080" },
               new MyTask { TaskName = "Mow the lawn", Completed = false, CategoryId = 2, TaskColor = "#008000" },
               new MyTask { TaskName = "Clear the gutters", Completed = false, CategoryId = 2, TaskColor = "#008000" },
               new MyTask { TaskName = "Laundray", Completed = true, CategoryId = 2, TaskColor = "#008000" },
               new MyTask { TaskName = "Pick up groceries from Pak N Save", Completed = false, CategoryId = 3, TaskColor = "#0000FF" },
               new MyTask { TaskName = "Farmers Market", Completed = false, CategoryId = 3, TaskColor = "#0000FF" }
            };

            foreach (var category in categories)
            {
                await SaveCategoryAsync(category);
            }

            foreach (var task in tasks)
            {
                await SaveTaskAsync(task);
            }
        }
    }
}
