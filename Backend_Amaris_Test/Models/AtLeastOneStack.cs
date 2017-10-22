using Backend_Amaris_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Backend_Amaris_Test.Models
{
    public class AtLeastOneStack : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (((IEnumerable<StackViewModel>)value).Any(x => x.IsSelected))
                return ValidationResult.Success;
            else
                return new ValidationResult("Select at least one stack");
        }
    }
}