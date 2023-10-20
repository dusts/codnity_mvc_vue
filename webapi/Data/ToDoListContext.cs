using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
        }

        public DbSet<ToDoListItem> ToDoListItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoListItem>().ToTable("ToDoListItem");
        }
    }
}
