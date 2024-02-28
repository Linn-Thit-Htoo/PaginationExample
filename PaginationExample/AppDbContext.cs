using Microsoft.EntityFrameworkCore;
using PaginationExample.Models;

namespace PaginationExample
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
