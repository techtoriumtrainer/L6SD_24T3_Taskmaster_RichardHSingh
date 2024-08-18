using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskNoter.MVVM.Models;
using System.Collections.ObjectModel;


namespace TaskNoter.Service
{
    public class DBService
    {
        SQLiteAsyncConnection TNDatabase;

        public DBService(string dbPath)
        {
            // initialise DB and creating tables
            TNDatabase = new SQLiteAsyncConnection(dbPath);

            TNDatabase.CreateTableAsync<MyTask>().Wait();
            TNDatabase.CreateTableAsync<Category>().Wait();
        }

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

        public async Task<int> SaveCategoryAsync(Category cat)
        {
            await Init();
            if (cat.Id != 0)
            {
                return await TNDatabase.UpdateAsync(cat);
            }
            else
            {
                return await TNDatabase.InsertAsync(cat);
            }
        }


        public async Task<int> DeleteTaskAsync(MyTask task)
        {
            await Init();

            return await TNDatabase.DeleteAsync(task);
        }

        public async Task<int> DeleteCategoryAsync(Category cat)
        {
            await Init();
            return await TNDatabase.DeleteAsync(cat);
        }

        public async Task FillData()
        {
            var categories = new List<Category>
            {
                // number of category along with id, the name (not yet assigned) and colour representation
                new Category
                {
                    CategoryName = "Watch Anime",
                    Color = "#800080"
                },
                new Category
                {
                    CategoryName = "Daily Chores",
                    Color = "#008000"
                },
                new Category
                {
                    CategoryName = "Outside Tasks",
                    Color = "#0000FF"
                }
            };

            var tasks = new List<MyTask>
            {
               new MyTask
                {
                    TaskName = "Create a watch list",
                    Completed = false,
                    CategoryId = 1,
                    TaskColor = "#800080"
                },
                new MyTask
                {
                    TaskName = "Finish Fate Series",
                    Completed = true,
                    CategoryId = 1,
                    TaskColor = "#800080"
                },
                new MyTask
                {
                    TaskName = "Mow the lawn ",
                    Completed = false,
                    CategoryId = 2,
                    TaskColor = "#008000"
                },
                new MyTask
                {
                    TaskName = "Clear the gutters",
                    Completed = false,
                    CategoryId = 2,
                    TaskColor = "#008000"
                },
                new MyTask
                {
                    TaskName = "Laundray",
                    Completed = true,
                    CategoryId = 2,
                    TaskColor = "#008000"
                },
                new MyTask
                {
                    TaskName = "Pick up groceries from Pak N Save",
                    Completed = false,
                    CategoryId = 3,
                    TaskColor = "#0000FF"
                },
                new MyTask
                {
                    TaskName = "Famers Market",
                    Completed = false,
                    CategoryId = 3,
                    TaskColor = "#0000FF"
                },
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
