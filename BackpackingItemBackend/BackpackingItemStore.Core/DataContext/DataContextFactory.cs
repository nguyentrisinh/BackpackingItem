using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace BackpackingItemStore.Core.DataContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-SOSSAL0; Database=BackpackingStore; Trusted_Connection=True; MultipleActiveResultSets=true");

            return new DataContext(optionsBuilder.Options);
        }

    }
}
