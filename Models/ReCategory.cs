using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReCategory
    {
        public ReCategory()
        {
            ReProducts = new HashSet<ReProduct>();
        }

        public decimal Id { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<ReProduct> ReProducts { get; set; }
    }
}
