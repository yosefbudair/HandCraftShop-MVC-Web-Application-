using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReCustomer
    {
        public ReCustomer()
        {
            ReProductCustomers = new HashSet<ReProductCustomer>();
            ReUserLogins = new HashSet<ReUserLogin>();
        }

        public decimal Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<ReProductCustomer> ReProductCustomers { get; set; }
        public virtual ICollection<ReUserLogin> ReUserLogins { get; set; }
    }
}
