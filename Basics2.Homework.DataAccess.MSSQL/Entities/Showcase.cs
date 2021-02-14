using System;
using System.ComponentModel.DataAnnotations;

namespace Basics2.Homework.DataAccess.MSSQL.Entities
{
    public class Showcase
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public short Volume { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> RemoveDate { get; set; }
    }
}
