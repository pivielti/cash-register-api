using System;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Domain.Products
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})")]
        public string Color { get; set; }
    }
}
