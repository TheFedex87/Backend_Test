using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Backend_Amaris_Test.Models
{
    public class WebDeveloper
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string ContactPhone { get; set; }

        [Required]
        [Display(Name = "Day of Birth")]
        public DateTime DayOfBirth { get; set; }

        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50")]
        public int YearsOfExperience { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public List<int> WebTechnologyId { get; set; }
        public List<WebTechnology> WebTechnologies { get; set; }

        public List<int> StackId { get; set; }
        public List<Stack> Stacks { get; set; }
    }
}