using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace MVCFirstApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Brand")]
        [MaxLength(30)]
        public string Brand { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Date of construction")]
        public int YearOfConstruction { get; set; }
        [Required]
        [Display(Name ="Kilometres driven")]
        [Range(1, 1000000)]
        public int KilometresDriven { get; set; }
        [Required]
        [Display(Name = "Power in kiloWatts")]
        [Range(30, 400)]
        public int PowerInKilowatts { get; set; }
        [Required]
        [Range(1, 4000000)]
        public double Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
