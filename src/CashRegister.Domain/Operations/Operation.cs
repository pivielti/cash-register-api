using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CashRegister.Domain.Operations
{
    public class Operation
    {
        public Operation()
        {
            Details = new HashSet<Detail>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Date and time")]
        public DateTime Moment { get; set; }

        [Required]
        [Display(Name = "Cost price")]
        public bool Reduced { get; set; }

        [Required]
        [Display(Name = "Operation details")]
        public virtual ICollection<Detail> Details { get; set; }

        [NotMapped]
        [Display(Name = "Total")]
        public decimal Total => Details.Sum(x => x.TotalPrice);
    }
}
