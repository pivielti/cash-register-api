using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Domain.Operations
{
    public class Detail
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Display(Name = "Product price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "How many ?")]
        public int Count { get; set; }

        public int OperationId { get; set; }

        [Display(Name = "Operation")]
        public virtual Operation Operation { get; set; }

        [NotMapped]
        [Display(Name = "Total")]
        public decimal TotalPrice => ProductPrice * Count;
    }
}
