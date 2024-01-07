using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReUserLogin
    {
        public decimal Id { get; set; }
        public string UserName { get; set; }
        public string Passwordd { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? CustomerId { get; set; }

        public virtual ReCustomer Customer { get; set; }
        public virtual ReRole Role { get; set; }
    }
}
