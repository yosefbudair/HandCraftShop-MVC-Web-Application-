using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Models
{
    public partial class Creditcard
    {
        public decimal Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Expire { get; set; }
        public decimal? Cvv { get; set; }
        public decimal? Credit { get; set; }
    }
}
