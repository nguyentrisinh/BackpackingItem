using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackpackingItemBackend.Models;

namespace BackpackingItemBackend.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            #region ModelConfiguration

            #region ApplicationUser Configuration 
            builder.Entity<ApplicationUser>().HasKey(ent => ent.Id);

            #endregion

            #region Category Configuration
            builder.Entity<Category>().HasKey(ent => ent.Id);

            #endregion

            #region SubCategory Configuration
            builder.Entity<SubCategory>().HasKey(ent => ent.Id);

            builder.Entity<SubCategory>().HasOne(ent => ent.Category).WithMany(ent => ent.SubCategories).HasForeignKey(ent => ent.CategoryId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Supplier Configuration
            builder.Entity<Supplier>().HasKey(ent => ent.Id);
            #endregion

            #region Product Configuration
            builder.Entity<Product>().HasKey(ent => ent.Id);
            builder.Entity<Product>().HasOne(ent => ent.Supplier).WithMany(ent => ent.Products).HasForeignKey(ent => ent.SupplierId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>().HasOne(ent => ent.SubCategory).WithMany(ent => ent.Products).HasForeignKey(ent => ent.SubCategoryId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Size Configuration
            builder.Entity<Size>().HasKey(ent => ent.Id);
            #endregion

            #region Color Configuration
            builder.Entity<Color>().HasKey(ent => ent.Id);
            #endregion

            #region Variant Configuration
            builder.Entity<Variant>().HasKey(ent => ent.Id);

            builder.Entity<Variant>().HasOne(ent => ent.Product).WithMany(ent => ent.Variants).HasForeignKey(ent => ent.ProductId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Variant>().HasOne(ent => ent.Color).WithMany(ent => ent.Variants).HasForeignKey(ent => ent.ColorId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

            // Test Without SizeId such as item like camera
            builder.Entity<Variant>().HasOne(ent => ent.Size).WithMany(ent => ent.Variants).HasForeignKey(ent => ent.SizeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Image Configuration
            builder.Entity<Image>().HasKey(ent => ent.Id);

            builder.Entity<Image>().HasOne(ent => ent.Variant).WithMany(ent => ent.Images).HasForeignKey(ent => ent.VariantId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Voucher Configuration
            builder.Entity<Voucher>().HasKey(ent => ent.Id);

            #endregion

            #region City Configuration
            builder.Entity<City>().HasKey(ent => ent.Id);
            #endregion

            #region District Configuration
            builder.Entity<District>().HasKey(ent => ent.Id);
            builder.Entity<District>().HasOne(ent => ent.City).WithMany(ent => ent.Districts).HasForeignKey(ent => ent.CityId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Order Configuration
            builder.Entity<Order>().HasKey(ent => ent.Id);

            builder.Entity<Order>().HasOne(ent => ent.District).WithMany(ent => ent.Orders).HasForeignKey(ent => ent.DistrictId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Order>().HasOne(ent => ent.Customer).WithMany(ent => ent.CustomerOrders).HasForeignKey(ent => ent.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Order>().HasOne(ent => ent.Voucher).WithMany(ent => ent.Orders).HasForeignKey(ent => ent.VoucherId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region OrderDetail Configuration
            builder.Entity<OrderDetail>().HasKey(ent => ent.Id);

            builder.Entity<OrderDetail>().HasOne(ent => ent.Variant).WithMany(ent => ent.OrderDetails).HasForeignKey(ent => ent.VariantId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderDetail>().HasOne(ent => ent.Order).WithMany(ent => ent.OrderDetails).HasForeignKey(ent => ent.OrderId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region ShipmentInfo Configuration
            builder.Entity<ShipmentInfo>().HasKey(ent => ent.Id);

            builder.Entity<ShipmentInfo>().HasOne(ent => ent.Customer).WithMany(ent => ent.ShipmentInfos).HasForeignKey(ent => ent.CustomerId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ShipmentInfo>().HasOne(ent => ent.District).WithMany(ent => ent.ShipmentInfos).HasForeignKey(ent => ent.DistrictId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion
            #endregion

        }

        #region DbSet
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShipmentInfo> ShipmentInfos { get; set; }
        #endregion

    }
}
