using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BackpackingItemBackend.DataContext;
using Microsoft.Extensions.Configuration;

namespace BackpackingItemBackend.DataContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BackpackingStore;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-SOSSAL0;Database=BackpackingStore;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
