using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class Order
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public decimal? CraftId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Numpieces { get; set; }
        public DateTime? Dateorders { get; set; }

        public virtual Craft Craft { get; set; }
        public virtual User User { get; set; }
    }
}
