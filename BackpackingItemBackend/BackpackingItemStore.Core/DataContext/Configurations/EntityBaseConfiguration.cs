using System;
using System.Collections.Generic;
using System.Text;
using BackpackingItemStore.Core.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BackpackingItemStore.Core.DataContext.Configurations
{
    public abstract class EntityBaseConfiguration<T> where T : GuidEntityBase
    {
        public EntityBaseConfiguration(ModelBuilder builder)
        {
            builder.Entity<T>().HasKey(e => e.Id).HasAnnotation("SqlServer:Clustered", false);
            builder.Entity<T>().HasIndex(e => e.SecondaryId).ForSqlServerIsClustered();

            builder.Entity<T>().Property(p => p.SecondaryId)
                .UseSqlServerIdentityColumn();
            builder.Entity<T>().Property(p => p.SecondaryId)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            builder.Entity<T>(Configure);
        }

        public abstract void Configure(EntityTypeBuilder<T> typeBuilder);
    }

    public static class ConfigurationExtension
    {
        public static void ConfigureModel<T>(this ModelBuilder builder)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), builder);
        }
    }
}
