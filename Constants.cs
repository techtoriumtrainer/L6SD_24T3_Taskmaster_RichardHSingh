using System;
using System.IO;

namespace TaskNoter
{
    public static class Constants
    {
        // ====================== NAME GIVEN TO DATABASE =======================
        public const string DB_Name = "Taskmaster.db3";

        // ======================== DATABASE PATH ========================
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DB_Name);

        // ======================== SQL LIBRARY OPTIONS ========================
        public const SQLite.SQLiteOpenFlags Flags =

            // Open the database in either read or write mode
            SQLite.SQLiteOpenFlags.ReadWrite |

            // Creates the database if it doesn't already exist
            SQLite.SQLiteOpenFlags.Create |

            // Enables multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
    }
}
