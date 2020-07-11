using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TLTTSaaSWebApp.Models
{
    public class Talent
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z-_. ]+$", ErrorMessage = "Symbols and numbers are not allowed.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z-_. ]+$", ErrorMessage = "Symbols and numbers are not allowed.")]
        public string ShortName { get; set; }

        [Required]
        public string Reknown { get; set; }

        [Required]
        public string Bio { get; set; }
    }
}