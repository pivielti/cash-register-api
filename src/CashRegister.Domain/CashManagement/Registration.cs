using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Domain.CashManagement
{
    public class Registration : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Moment { get; set; }

        [Required]
        [Display(Name = "Registration type")]
        public RegistrationType Type { get; set; }

        [Required]
        [Display(Name = "Cash amount")]
        public decimal Cash { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Cash <= 0)
                yield return new ValidationResult("The amount should be greater than zero !", new[] { nameof(Cash) });
        }
    }
}