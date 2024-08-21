using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskNoter.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MyTask
    {
        //auto increment the PK for ID
        [PrimaryKey, AutoIncrement]


        // id to give to task
        public int Id { get; set; }

        // string for what the taskname would be
        public string TaskName { get; set; }

        // can check the box once task is completed
        public bool Completed { get; set; }

        // which category it can be assigned to and appropriate colour
        public int CategoryId { get; set; }
        public string TaskColor { get; set; }
    }
}
