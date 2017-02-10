using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ControlCenter.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<GroceryList> GroceryLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ControlCenter.db");
        }
    }
}
