using DemoOCR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DemoOCR.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {


        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<Registration> registrations { get; set; }

    }
}
