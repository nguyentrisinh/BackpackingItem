using System;
using System.Collections.Generic;
using System.Text;
using BackpackingItemStore.Core.DataContext.Configurations;
using BackpackingItemStore.Core.Models;
using BackpackingItemStore.Core.Extensions;
using BackpackingItemStore.Core.Models.Abstract;
using BackpackingItemStore.Core.Models.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackpackingItemStore.Core.DataContext
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
                
            #region ApplicationUser Configuration

            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<ApplicationUser>().HasKey(e => e.Id).HasAnnotation("SqlServer:Clustered", false);
            builder.Entity<ApplicationUser>().HasIndex(e => e.SecondaryId).ForSqlServerIsClustered();

            builder.Entity<ApplicationUser>().Property(p => p.SecondaryId)
                .UseSqlServerIdentityColumn();
            builder.Entity<ApplicationUser>().Property(p => p.SecondaryId)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            #endregion

            builder.Ignore<GuidEntityBase>();
            builder.Ignore<EntityBase<Guid, long>>();

            builder.ConfigureModel<CategoryConfiguration>();
        }

        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries<IAudit>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = HiResDateTime.UtcNow;
                        entry.Entity.ModifiedDate = HiResDateTime.UtcNow;
                        entry.Entity.CreatedAt = (long)(entry.Entity.CreatedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                        entry.Entity.ModifiedAt = (long)(entry.Entity.ModifiedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                        break;
                    case EntityState.Modified:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Entity.ModifiedDate = HiResDateTime.UtcNow;
                        entry.Entity.ModifiedAt = (long)(entry.Entity.ModifiedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                        break;
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex.InnerException.InnerException ?? ex.InnerException;
            }
        }


    }
}
