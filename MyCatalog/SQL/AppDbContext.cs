using Microsoft.EntityFrameworkCore;
using MyCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatalog.SQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        public DbSet <WishListItem> WishListItems { get; set; }

    }
}
