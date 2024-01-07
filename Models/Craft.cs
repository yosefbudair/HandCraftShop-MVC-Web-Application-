using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace Project.Models
{
    public partial class Craft
    {
        public Craft()
        {
            Orders = new HashSet<Order>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        [Display(Name = "Crafts Name"), Required]
        public string Name { get; set; }
        [Display(Name = "Crafts Price"), Required]
        public decimal? Price { get; set; }
        public decimal? Sales { get; set; }
        public DateTime? Datecreated { get; set; }
        public string Description { get; set; }
        public string Imagepath { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? UserId { get; set; }
        [Display(Name = "CategoryId"), Required]
        public decimal? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
