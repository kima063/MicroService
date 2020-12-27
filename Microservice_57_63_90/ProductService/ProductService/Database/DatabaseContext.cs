using Microsoft.EntityFrameworkCore;
using ProductService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Products> Products { get; set; }

        public DbSet<Category> Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-TJGR4TL\SQLEXPRESS; initial catalog= Products;
            persist security info=True;Integrated Security=SSPI;");
        }
    }
}
