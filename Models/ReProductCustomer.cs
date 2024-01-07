using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReProductCustomer
    {
        public decimal Id { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? CustomerId { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual ReCustomer Customer { get; set; }
        public virtual ReProduct Product { get; set; }
    }
}
