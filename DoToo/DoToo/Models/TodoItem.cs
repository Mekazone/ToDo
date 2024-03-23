using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace DoToo.Models
{
    //model for accessing the database
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime Due { get; set; }
    }
}
