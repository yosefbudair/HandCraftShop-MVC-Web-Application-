using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public decimal? CraftId { get; set; }
        public string Coomment { get; set; }
        public decimal? Visable { get; set; }

        public virtual Craft Craft { get; set; }
        public virtual User User { get; set; }
    }
}
