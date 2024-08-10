using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskNoter
{
    public static class Constants
    {
        private const string DB_Name = "TaskNoter_DB.db3";

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DB_Name);


        
        // ======================== CRUD FOR TASKS ========================
        // ================================================================
        public const SQLite.SQLiteOpenFlags Flags =

           // opens the database in read/write mode
           SQLite.SQLiteOpenFlags.ReadWrite |

           // create the database if it doesn't already exist
           SQLite.SQLiteOpenFlags.Create |

           // enable multi-threaded database access
           SQLite.SQLiteOpenFlags.SharedCache;
    }
}
