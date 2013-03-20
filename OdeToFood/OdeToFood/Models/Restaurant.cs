using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(60)]
        public string City { get; set; }
        
        [Required]
        [StringLength(60)]
        public string Country { get; set; }
        
        public virtual ICollection<RestaurantReview> Reviews { get; set; }
    }
}