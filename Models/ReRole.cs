using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class ReRole
    {
        public ReRole()
        {
            ReUserLogins = new HashSet<ReUserLogin>();
        }

        public decimal Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<ReUserLogin> ReUserLogins { get; set; }
    }
}
