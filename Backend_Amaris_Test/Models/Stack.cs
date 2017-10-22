using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Backend_Amaris_Test.Models
{
    public class Stack
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<int> WebDeveloperId { get; set; }
        public List<WebDeveloper> WebDevelopers { get; set; }
    }
}