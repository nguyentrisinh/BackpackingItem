using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BackpackingItemBackend.DataContext;

namespace BackpackingItemBackend.DataContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BackpackingStore;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer("Server=DESKTOP-SOSSAL0;Database=BackpackingStore;Trusted_Connection=True;MultipleActiveResultSets=true");


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
