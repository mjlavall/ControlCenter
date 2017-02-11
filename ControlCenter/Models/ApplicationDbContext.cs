using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ControlCenter.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<ShoppingUnit> ShoppingUnits { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ControlCenter.db");
        }
    }
}
