using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project.Models
{
    public partial class Category
    {
        public Category()
        {
            Crafts = new HashSet<Craft>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Craft> Crafts { get; set; }
    }
}
