using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BackpackingItemBackend.DataContext
{
    public interface IDbinitializer
    {
        void Init();
    }

    public class Dbinitializer : IDbinitializer
    {
        private readonly ApplicationDbContext mDataContext;
        private readonly UserManager<ApplicationUser> mUserManager;
        private readonly RoleManager<IdentityRole> mRoleManager;

        public Dbinitializer(ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            mDataContext = dataContext;
            mUserManager = userManager;
            mRoleManager = roleManager;
        }

        public void Init()
        {
            mDataContext.Database.Migrate();


            if (!mDataContext.Database.GetPendingMigrations().Any())
            {
                #region categories
                var categories = SeedCategory();
                #endregion

                #region subCategories
                var subCategories = SeedSubCategory(categories);
                #endregion

                #region suppliers
                var suppliers = SeedSupplier();
                #endregion

                #region products
                var products = SeedProduct(suppliers, subCategories);
                #endregion

                // Save Changes in Database
                mDataContext.SaveChanges();
            }

        }

        #region Seed Category

        public List<Category> SeedCategory ()
        {
            if (!mDataContext.Set<Category>().Any())
            {
                var categories = new List<Category>()
                {
                    new Category() { Name = "Mũ bảo hiểm" },
                    new Category() { Name = "Áo giáp | khoác" },
                    new Category() { Name = "Găng tay" }
                };

                mDataContext.Set<Category>().AddRange(categories);
                return categories;

            } else
            {
                var categories = mDataContext.Categories.ToList();
                return categories;
            }

        }
        #endregion

        #region Seed SubCategory
        public List<SubCategory> SeedSubCategory (List<Category> categories)
        {
            if(!mDataContext.Set<SubCategory>().Any())
            {
                var subCategories = new List<SubCategory>()
                {
                    new SubCategory() { Name = "Mũ bảo hiểm 3/4", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Name = "Mũ bảo hiểm Fullface", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Name = "Mũ bảo hiểm cào cào", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Name = "Đồ bảo hộ và phụ kiện xe máy Dainese", CategoryId = categories.Single(ent => ent.Name == "Áo giáp | khoác").Id },
                    new SubCategory() { Name = "Áo giáp", CategoryId = categories.Single(ent => ent.Name == "Áo giáp | khoác").Id },

                };

                mDataContext.Set<SubCategory>().AddRange(subCategories);
                return subCategories;
            } else
            {
                var subCategories = mDataContext.SubCategories.ToList();
                return subCategories;
            }
        }
        #endregion

        #region Seed Supplier
        public List<Supplier> SeedSupplier ()
        {
            //if(!mDataContext.Set<Supplier>().Any())
            //{
                var suppliers = new List<Supplier>()
                {
                    new Supplier() { Name = "Dainese", Country = "USA" },
                    new Supplier() { Name = "Alpinestars", Country = "Italia" },
                    new Supplier() { Name = "Cree", Country = "USA" },
                    new Supplier() { Name = "GoPro", Country = "USA" },
                    new Supplier() { Name = "AGV", Country = "Italia" }
                };
                mDataContext.Set<Supplier>().AddRange(suppliers);
            mDataContext.SaveChanges();

            return suppliers;

            //} else
            //{
            //    var suppliers = mDataContext.Set<Supplier>().ToList();
            //    return suppliers;
            //}
        }
        #endregion

        #region Seed Product
        public List<Product> SeedProduct(List<Supplier> suppliers, List<SubCategory> subCategories)
        {
            //if (!mDataContext.Set<Product>().Any())
            //{
                #region First Product
                var product1 = new Product()
                {
                    Name = "AGV Fluid Garda White Italy",
                    ImageUrl = "/StaticFiles/MyImages/agv-fluid-garda-white-italia-helmet-2-800x800.jpg",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm\n ĐẶC TÍNH KỸ THUẬT\n 1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau.\n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở.\n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng.\n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao.\n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ ",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm 3/4").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product2 = new Product()
                {
                    Name = "AGV Fluid IBISCUS",
                    ImageUrl = "/StaticFiles/MyImages/agv-fluid-ibiscus-1-800x800.jpg",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm 3/4").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product3 = new Product()
                {
                    Name = "AGV K-3 SV Balloon 2018 (Fullface 2 kính)",
                    ImageUrl = "/StaticFiles/MyImages/agv-k3-sv-balloon-2018-asian-fit-helmet-5-800x800.jpg",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm Fullface").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product4 = new Product()
                {
                    Name = "AGV K - 3 SV Joan MIR 2018(Fullface 2 kính)",
                    ImageUrl = "/StaticFiles/MyImages/agv-k3-sv-joan-mir-2018-fullface-helmet-0-800x800.jpg",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm Fullface").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };
                #endregion

                var products = new List<Product>()
                {
                    product1,
                    product2,
                    product3,
                    product4
                };

                mDataContext.Set<Product>().AddRange(products);
                return products;
            //} else
            //{
            //    var products = mDataContext.Set<Product>().ToList();
            //    return products;
            //}
        }
        #endregion

    }
}
