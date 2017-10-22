using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backend_Amaris_Test.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {

        }

        public DbSet<Stack> Stack { get; set; }
        public DbSet<WebDeveloper> WebDeveloper { get; set; }
        public DbSet<WebTechnology> WebTechnology { get; set; }
    }
}