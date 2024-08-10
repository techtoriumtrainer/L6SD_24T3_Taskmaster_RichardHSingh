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


        public int Id { get; set; }
        public string TaskName { get; set; }
        public bool Completed { get; set; }
        public int CategoryId { get; set; }
        public string TaskColor { get; set; }
    }
}
