using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Domain.CashManagement
{
    public enum RegistrationType
    {
        [Display(Name = "Opening")]
        Opening,

        [Display(Name = "Closing")]
        Closing
    }
}