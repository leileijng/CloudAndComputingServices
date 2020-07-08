using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression("([a-zA-Z0-9_ ]+)", ErrorMessage = "Alphanumeric (including underscore and space)")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression("([a-zA-Z0-9_ ]+)", ErrorMessage = "Alphanumeric (including underscore and space)")]
        public string Category { get; set; }
        [Required]
        [Range(0,100)]
        public decimal Price { get; set; }
    }
}