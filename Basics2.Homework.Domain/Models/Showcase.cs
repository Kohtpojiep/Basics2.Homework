using System;

namespace Basics2.Homework.Domain.Models
{
    public class Showcase
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public short Volume { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }
    }
}
