using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Backend_Amaris_Test.ViewModel;

namespace Backend_Amaris_Test.Models
{
    public class AtLeastOneWebTechnology : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (((IEnumerable<WebTechnologyViewModel>)value).Any(x => x.IsSelected))
                return ValidationResult.Success;
            else
                return new ValidationResult("Select at least one web technology");
        }
    }
}