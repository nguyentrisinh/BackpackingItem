using System;
using System.Collections.Generic;
using System.Text;
using BackpackingItemStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackpackingItemStore.Core.DataContext.Configurations
{
    class CategoryConfiguration : EntityBaseConfiguration<Category>
    {
        public CategoryConfiguration(ModelBuilder builder) : base(builder)
        {
            // Nothing in here
        }

        public override void Configure(EntityTypeBuilder<Category> typeBuilder)
        {
            // Category has nothing here
        }
    }
}
