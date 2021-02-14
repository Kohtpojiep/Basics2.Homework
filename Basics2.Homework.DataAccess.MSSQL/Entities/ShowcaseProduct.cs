using System;
using System.Collections.Generic;
using System.Text;

namespace Basics2.Homework.DataAccess.MSSQL.Entities
{
    public class ShowcaseProduct
    {
        public int Id { get; set; } 
        public int ShowcaseId { get; set; }
        public int ProductId { get; set; }
        public short ProductCount { get; set; }
        public decimal ProductCost { get; set; }

        public virtual Showcase Showcase { get; set; }
        public virtual Product Product { get; set; }
    }
}
