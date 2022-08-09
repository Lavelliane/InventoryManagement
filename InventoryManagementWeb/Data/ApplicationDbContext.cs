using InventoryManagementWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}
