using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace Project.Models
{
    public partial class User
    {
        public User()
        {
            Crafts = new HashSet<Craft>();
            Orders = new HashSet<Order>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        [Display(Name = "UserName"), Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Password"), Required]
        public string Passwordd { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime? Datecreated { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? Isaccepted { get; set; }


        public virtual Role Role { get; set; }
        public virtual ICollection<Craft> Crafts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
