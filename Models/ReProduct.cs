using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReProduct
    {
        public ReProduct()
        {
            ReProductCustomers = new HashSet<ReProductCustomer>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Price { get; set; }
        public decimal? CategoryId { get; set; }

        public virtual ReCategory Category { get; set; }
        public virtual ICollection<ReProductCustomer> ReProductCustomers { get; set; }
    }
}
