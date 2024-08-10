using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TaskNoter.MVVM.Models;


namespace TaskNoter
{
    public class TaskNoterDB
    {
        SQLiteAsyncConnection TNDatabase;

        public TaskNoterDB()
        {

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

            if(task.Id != 0)
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
        public async Task<int> SaveCategoryAsync(Category categories)
        {
            await Init();

            if (categories.Id != 0)
            {
                return await TNDatabase.UpdateAsync(categories);
            }
            else
            {
                return await TNDatabase.InsertAsync(categories);
            }
        }

        public async Task<int> DeleteCategoryAsync(Category categories)
        {
            await Init();

            return await TNDatabase.DeleteAsync(categories);
        }
    }
}
