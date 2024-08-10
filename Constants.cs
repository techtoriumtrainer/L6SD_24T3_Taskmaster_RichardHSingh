using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskNoter
{
    public static class Constants
    {
        // ====================== NAME GIVEN TO DATABASE =======================
        // =====================================================================
        public const string DB_Name = "TaskNoter_DB.db3";


        // ======================== DATABASE PATH ========================
        // ===============================================================
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DB_Name);


        // ======================== SQL LIBRARY OPTIONS ========================
        // =====================================================================
        public const SQLite.SQLiteOpenFlags Flags =

            // open the database in either read or write mode
            SQLite.SQLiteOpenFlags.ReadWrite |

            // creates the database if it doesn't already exist
            SQLite.SQLiteOpenFlags.Create |

            // enables multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;        
    }
}
