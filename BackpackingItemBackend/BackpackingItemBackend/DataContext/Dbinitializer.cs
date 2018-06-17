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
            //mDataContext.Database.Migrate();


            if (!mDataContext.Database.GetPendingMigrations().Any())
            {
                #region categories
                var categories = SeedCategory();

                #region save categories
                mDataContext.Database.OpenConnection();
                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories ON");

                mDataContext.SaveChanges();

                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories OFF");
                mDataContext.Database.CloseConnection();
                #endregion
                #endregion

                #region subCategories
                var subCategories = SeedSubCategory(categories);

                #region save sub categories
                mDataContext.Database.OpenConnection();
                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SubCategories ON");

                mDataContext.SaveChanges();

                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SubCategories OFF");
                mDataContext.Database.CloseConnection();
                #endregion
                #endregion

                #region suppliers
                var suppliers = SeedSupplier();
                #endregion

                #region products
                var products = SeedProduct(suppliers, subCategories);
                #endregion

                #region color
                var colors = SeedColor();
                #endregion

                #region size
                var sizes = SeedSize();
                #endregion

                #region variant
                var variants = SeedVariant(colors, sizes, products);

                #region save variants
                mDataContext.Database.OpenConnection();
                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Variants ON");

                mDataContext.SaveChanges();

                mDataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Variants OFF");
                mDataContext.Database.CloseConnection();
                #endregion
                #endregion

                #region images
                var images = SeedImages();

                #endregion

                #region city
                var cities = SeedCity();
                #endregion

                #region district
                var districts = SeedDistrict(cities);
                #endregion

                #region voucher
                var vouchers = SeedVoucher();
                #endregion

                #region IDENTITY_INSERT configuration and SaveChanges
                // Save Changes in Database
                mDataContext.SaveChanges();
                #endregion
            }

        }

        #region Seed Category

        public List<Category> SeedCategory ()
        {
            if (!mDataContext.Set<Category>().Any())
            {
                var categories = new List<Category>()
                {
                    new Category() { Id = 1,Name = "Mũ bảo hiểm" },
                    new Category() { Id=2,Name = "Quần áo phượt" },
                    new Category() { Id=3,Name = "Phụ kiện phượt" },
                    new Category() { Id=4,Name = "Công cụ phượt" },
                    new Category() { Id=5,Name = "Giày" },
                    new Category() { Id=6,Name = "Balo - Túi" },
                    new Category() { Id=7,Name = "Thiết bị công nghệ" }
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
                    new SubCategory() { Id=1,Name = "Mũ bảo hiểm 3/4 đầu", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Id=24,Name = "Mũ bảo hiểm Fullface", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Id=25,Name = "Mũ bảo hiểm cào cào", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Id=26,Name = "Kính gắn mũ bảo hiểm", CategoryId = categories.Single(ent => ent.Name == "Mũ bảo hiểm").Id },
                    new SubCategory() { Id=27,Name = "Áo bảo hộ", CategoryId = categories.Single(ent => ent.Name == "Quần áo phượt").Id },
                    new SubCategory() { Id=28,Name = "Quần bảo hộ", CategoryId = categories.Single(ent => ent.Name == "Quần áo phượt").Id },
                    new SubCategory() { Id=29,Name = "Quần áo chống thấm nước", CategoryId = categories.Single(ent => ent.Name == "Quần áo phượt").Id },
                    new SubCategory() {Id=30, Name = "Quần áo nhanh khô", CategoryId = categories.Single(ent => ent.Name == "Quần áo phượt").Id },
                    new SubCategory() { Id=31,Name = "Quần áo phượt khác", CategoryId = categories.Single(ent => ent.Name == "Quần áo phượt").Id },
                    new SubCategory() { Id=4,Name = "Giày đi phượt", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=5,Name = "Giày leo núi", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=6,Name = "Giày lội suối", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=7,Name = "Giày đi xe, motor", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=8,Name = "Sandal phượt", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=9,Name = "Tất, vớ, phụ kiện khác", CategoryId = categories.Single(ent => ent.Name == "Giày").Id },
                    new SubCategory() { Id=11,Name = "Balo Laptop", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=20,Name = "Balo Máy ảnh", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=12,Name = "Balo Leo núi", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=13,Name = "Túi gác xe", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=14,Name = "Vali kéo, hành lý", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=15,Name = "Balo khác", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=16,Name = "Phụ kiện Balo", CategoryId = categories.Single(ent => ent.Name == "Balo - Túi").Id },
                    new SubCategory() { Id=33,Name = "Giáp tay chân", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=42,Name = "Găng tay", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=34,Name = "Ống tay chống nắng", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=35,Name = "Khăn", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=36,Name = "Dây áo phản quang", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=37,Name = "Mũ", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=38,Name = "Khác", CategoryId = categories.Single(ent => ent.Name == "Phụ kiện phượt").Id },
                    new SubCategory() { Id=39,Name = "Lều", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=40,Name = "Túi ngủ", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=41,Name = "Đồ sinh tồn - cứu sinh", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=23,Name = "Thuyền - xuồng - kayak", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=32,Name = "Thùng - Bình", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=22,Name = "Ống nhòm", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=3,Name = "Móc", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=2,Name = "Dao đa năng", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=44,Name = "Khác", CategoryId = categories.Single(ent => ent.Name == "Công cụ phượt").Id },
                    new SubCategory() { Id=17,Name = "Camera hành trình", CategoryId = categories.Single(ent => ent.Name == "Thiết bị công nghệ").Id },
                    new SubCategory() { Id=18,Name = "Máy ảnh", CategoryId = categories.Single(ent => ent.Name == "Thiết bị công nghệ").Id },
                    new SubCategory() { Id=19,Name = "Đèn pin", CategoryId = categories.Single(ent => ent.Name == "Thiết bị công nghệ").Id },
                    new SubCategory() { Id=21,Name = "Pin - máy sạc", CategoryId = categories.Single(ent => ent.Name == "Thiết bị công nghệ").Id },
                    new SubCategory() { Id=43,Name = "Khác", CategoryId = categories.Single(ent => ent.Name == "Thiết bị công nghệ").Id },

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
            if(!mDataContext.Set<Supplier>().Any())
            {
                var suppliers = new List<Supplier>()
                {
                    new Supplier() { Name = "Dainese", Country = "USA" },
                    new Supplier() { Name = "Alpinestars", Country = "Italia" },
                    new Supplier() { Name = "Cree", Country = "USA" },
                    new Supplier() { Name = "GoPro", Country = "USA" },
                    new Supplier() { Name = "AGV", Country = "Italia" },
                    new Supplier() { Name = "OffRoad", Country = "USA" },
                    new Supplier() { Name = "Andes", Country = "USA" },
                    new Supplier() { Name = "Suzuki", Country = "USA" },
                    new Supplier() { Name = "Komine", Country = "Japan" },
                    new Supplier() { Name = "Madfox", Country = "Japan" },
                    new Supplier() { Name = "Chums", Country = "USA" },
                    new Supplier() { Name = "Columbia", Country = "USA" },
                };
                mDataContext.Set<Supplier>().AddRange(suppliers);
                return suppliers;

            } else
            {
                var suppliers = mDataContext.Set<Supplier>().ToList();
                return suppliers;
            }
        }
        #endregion

        #region SeedSize
        public List<Size> SeedSize()
        {
            if (!mDataContext.Set<Size>().Any())
            {
                var sizes = new List<Size>()
                {
                    new Size() { Name = "None"},
                    new Size() { Name = "Free Size"},
                    new Size() { Name = "XS"},
                    new Size() { Name = "S"},
                    new Size() { Name = "M"},
                    new Size() { Name = "L"},
                    new Size() { Name = "XL"},
                    new Size() { Name = "30"},
                    new Size() { Name = "31"},
                    new Size() { Name = "32"},
                    new Size() { Name = "33"},
                    new Size() { Name = "34"},
                    new Size() { Name = "35"},
                    new Size() { Name = "36"},
                    new Size() { Name = "37"},
                    new Size() { Name = "38"},
                    new Size() { Name = "39"},
                    new Size() { Name = "40"},
                    new Size() { Name = "41"},
                    new Size() { Name = "42"},
                    new Size() { Name = "43"},
                    new Size() { Name = "44"},
                    new Size() { Name = "45"},
                    new Size() { Name = "46"},
                    new Size() { Name = "47"},
                    new Size() { Name = "48"},
                    new Size() { Name = "49"},
                    new Size() { Name = "50"},
                };
                mDataContext.Set<Size>().AddRange(sizes);
                return sizes;

            }
            else
            {
                var sizes = mDataContext.Set<Size>().ToList();
                return sizes;
            }
        }

        #endregion

        #region SeedColor
        public List<Color> SeedColor()
        {
            if (!mDataContext.Set<Color>().Any())
            {
                var colors = new List<Color>()
                {
                    new Color() { Name = "Không",ColorCode = "#ffffff"},
                    new Color() { Name = "Trà",ColorCode = "#ffe866"},
                    new Color() { Name = "Ngà",ColorCode = "#DCDCDC"},
                    new Color() { Name = "Đỏ",ColorCode = "#FF0000"},
                    new Color() { Name = "Đen",ColorCode = "#000000"},
                    new Color() { Name = "Xanh da trời",ColorCode = "#6699FF"},
                    new Color() { Name = "Xanh lá cây",ColorCode = "#005500"},
                    new Color() { Name = "Đa sắc",ColorCode = "#ffffff"},
                    new Color() { Name = "Hồng",ColorCode = "#FF33FF"},
                    new Color() { Name = "Xanh neon",ColorCode = "#66FF33"},
                    new Color() { Name = "Nâu phối đỏ",ColorCode = "#8B4513"},
                    new Color() { Name = "Xanh rêu",ColorCode = "#003300"},
                    new Color() { Name = "Xám đen",ColorCode = "#708090"},
                    new Color() { Name = "Xám ghi",ColorCode = "#FAFAD2"},
                   
                };
                mDataContext.Set<Color>().AddRange(colors);
                return colors;

            }
            else
            {
                var colors = mDataContext.Set<Color>().ToList();
                return colors;
            }
        }

        #endregion

        #region SeedImage
        public List<Image> SeedImages()
        {
            if (!mDataContext.Set<Image>().Any())
            {
                var images = new List<Image>()
                {
                   new Image(){ VariantId = 1,ImageUrl = "/StaticFiles/MyImages/fanfan0kinh-andes-lop-db-cho-non-bao-hiem-visor-for-3-4-helmet-3_0061162.jpg"},
                   new Image(){VariantId = 2,ImageUrl = "/StaticFiles/MyImages/fanfan0kinh-andes-lop-db-cho-non-bao-hiem-visor-for-3-4-helmet-2_0061161.jpg"},
                   new Image(){VariantId = 3,ImageUrl = "/StaticFiles/MyImages/fanfan0kinh-andes-lop-db-cho-non-bao-hiem-visor-for-3-4-helmet-1_0061160.jpg"},
                   new Image(){VariantId = 4,ImageUrl = "/StaticFiles/MyImages/ao-khoac-giap-bao-ho-Suzuki-1.jpg"},
                   new Image(){VariantId = 5,ImageUrl = "/StaticFiles/MyImages/ao-khoac-giap-bao-ho-Suzuki-1.jpg"},
                   new Image(){VariantId = 6,ImageUrl = "/StaticFiles/MyImages/ao-khoac-giap-bao-ho-Suzuki-1.jpg"},
                   new Image(){VariantId = 7,ImageUrl = "/StaticFiles/MyImages/komine-pk-717.jpg"},
                   new Image(){VariantId = 10,ImageUrl = "/StaticFiles/MyImages/komine-pk-717.jpg"},
                   new Image(){VariantId = 8,ImageUrl = "/StaticFiles/MyImages/komine-pk-717-den.jpg"},
                   new Image(){VariantId = 9,ImageUrl = "/StaticFiles/MyImages/komine-pk-717-den.jpg"},
                   
                   new Image(){VariantId = 11,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-gio-nam-goretex-chong-tham-nuoc-madfox_0059215.jpg"},
                   new Image(){VariantId = 12,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-gio-nam-goretex-chong-tham-nuoc-madfox_0059215.jpg"},
                   new Image(){VariantId = 13,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-gio-nam-goretex-chong-tham-nuoc-madfox_0059215.jpg"},
                    new Image(){VariantId = 14,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-gio-nam-goretex-chong-tham-nuoc-madfox_0059215.jpg"},
                   new Image(){VariantId = 15,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-gio-nam-goretex-chong-tham-nuoc-madfox_0059215.jpg"},

                    new Image(){VariantId = 11,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox_0059217.jpg"},
                    new Image(){VariantId = 12,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox_0059217.jpg"},
                    new Image(){VariantId = 13,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox_0059217.jpg"},
                    new Image(){VariantId = 14,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox_0059217.jpg"},
                    new Image(){VariantId = 15,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox_0059217.jpg"},

                    new Image(){VariantId = 16,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg"},
                    new Image(){VariantId = 17,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg"},
                    new Image(){VariantId = 18,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg"},
                    new Image(){VariantId = 19,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg"},
                    new Image(){VariantId = 20,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg"},

                    new Image(){VariantId = 21,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-do_0059940.jpg"},
                    new Image(){VariantId = 22,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-do_0059940.jpg"},
                    new Image(){VariantId = 23,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-do_0059940.jpg"},
                    new Image(){VariantId = 24,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-do_0059940.jpg"},

                    new Image(){VariantId = 25,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-under-arm_0059941.jpg"},
                    new Image(){VariantId = 26,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-under-arm_0059941.jpg"},
                    new Image(){VariantId = 27,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-under-arm_0059941.jpg"},
                    new Image(){VariantId = 28,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-under-arm_0059941.jpg"},

                    new Image(){VariantId = 25,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-xanh-da_0059942.jpg"},
                    new Image(){VariantId = 26,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-xanh-da_0059942.jpg"},
                    new Image(){VariantId = 27,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-xanh-da_0059942.jpg"},
                    new Image(){VariantId = 28,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-xanh-da_0059942.jpg"},

                    new Image(){VariantId = 29,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox_0059943.jpg"},
                    new Image(){VariantId = 30,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox_0059943.jpg"},
                    new Image(){VariantId = 31,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox_0059943.jpg"},
                    new Image(){VariantId = 32,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox_0059943.jpg"},

                    new Image(){VariantId = 33,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg"},
                    new Image(){VariantId = 34,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg"},
                    new Image(){VariantId = 35,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg"},
                    new Image(){VariantId = 36,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg"},
                    new Image(){VariantId = 37,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg"},

                    new Image(){VariantId = 38,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox-hong_0059210.jpg"},
                    new Image(){VariantId = 39,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox-hong_0059210.jpg"},
                    new Image(){VariantId = 40,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox-hong_0059210.jpg"},
                    new Image(){VariantId = 41,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox-hong_0059210.jpg"},
                    new Image(){VariantId = 42,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox-hong_0059210.jpg"},
                    
                    new Image(){VariantId = 43,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nu-madfox_0059211.jpg"},
                    new Image(){VariantId = 44,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nu-madfox_0059211.jpg"},
                    new Image(){VariantId = 45,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nu-madfox_0059211.jpg"},
                    new Image(){VariantId = 46,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nu-madfox_0059211.jpg"},
                    new Image(){VariantId = 47,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nu-madfox_0059211.jpg"},

                    new Image(){VariantId = 48,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox_0059209.jpg"},
                    new Image(){VariantId = 49,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox_0059209.jpg"},
                    new Image(){VariantId = 50,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox_0059209.jpg"},
                    new Image(){VariantId = 51,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox_0059209.jpg"},
                    new Image(){VariantId = 52,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-nu-chong-tham-nuoc-madfox_0059209.jpg"},

                    new Image(){VariantId = 55,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-chong-nuoc-chums_0057906.jpg"},
                    new Image(){VariantId = 56,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-chong-nuoc-chums_0057906.jpg"},

                    new Image(){VariantId = 55,ImageUrl = "/StaticFiles/MyImages/fanfan2ao-gio-chong-tham-nuoc-chums-mat-trong_0057907.jpg"},
                    new Image(){VariantId = 56,ImageUrl = "/StaticFiles/MyImages/fanfan2ao-gio-chong-tham-nuoc-chums-mat-trong_0057907.jpg"},

                    new Image(){VariantId = 53,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-chong-tham-nuoc-chums-nau-do_0057908.jpg"},
                    new Image(){VariantId = 54,ImageUrl = "/StaticFiles/MyImages/fanfan0ao-khoac-chong-tham-nuoc-chums-nau-do_0057908.jpg"},

                    new Image(){VariantId = 53,ImageUrl = "/StaticFiles/MyImages/fanfan2vai-chong-nuoc-ao-gio_0057909.jpg"},
                    new Image(){VariantId = 54,ImageUrl = "/StaticFiles/MyImages/fanfan2vai-chong-nuoc-ao-gio_0057909.jpg"},

                    new Image(){VariantId = 57,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-reu_0060060.jpg"},
                    new Image(){VariantId = 58,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-reu_0060060.jpg"},
                    new Image(){VariantId = 59,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-reu_0060060.jpg"},
                    new Image(){VariantId = 60,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-reu_0060060.jpg"},

                    new Image(){VariantId = 61,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia_0060061.jpg"},
                    new Image(){VariantId = 62,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia_0060061.jpg"},
                    new Image(){VariantId = 63,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia_0060061.jpg"},
                    new Image(){VariantId = 64,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia_0060061.jpg"},

                    new Image(){VariantId = 65,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-be_0060059.jpg"},
                    new Image(){VariantId = 66,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-be_0060059.jpg"},
                    new Image(){VariantId = 67,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-be_0060059.jpg"},
                    new Image(){VariantId = 68,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia-be_0060059.jpg"},

                    new Image(){VariantId = 65,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-sort-du-lich-columbia_0060062.jpg"},
                    new Image(){VariantId = 66,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-sort-du-lich-columbia_0060062.jpg"},
                    new Image(){VariantId = 67,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-sort-du-lich-columbia_0060062.jpg"},
                    new Image(){VariantId = 68,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-sort-du-lich-columbia_0060062.jpg"},

                    new Image(){VariantId = 65,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-columbia_0060058.jpg"},
                    new Image(){VariantId = 66,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-columbia_0060058.jpg"},
                    new Image(){VariantId = 67,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-columbia_0060058.jpg"},
                    new Image(){VariantId = 68,ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-columbia_0060058.jpg"},
                };
                mDataContext.Set<Image>().AddRange(images);
                return images;

            }
            else
            {
                var images = mDataContext.Set<Image>().ToList();
                return images;
            }
        }

        #endregion

        #region Seed Variant
        public List<Variant> SeedVariant(List<Color> colors, List<Size> sizes,List<Product> products)
        {
            if (!mDataContext.Set<Variant>().Any())
            {
                #region Variants

                var variant77 = new Variant()
                {
                    Id = 77,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 5610000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "AGV Fluid Garda White Italy").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant76 = new Variant()
                {
                    Id = 76,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 6510000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "AGV Fluid IBISCUS").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant75 = new Variant()
                {
                    Id = 75,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 7050000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "AGV K-3 SV Balloon 2018 (Fullface 2 kính)").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant74 = new Variant()
                {
                    Id = 74,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 7250000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "AGV K - 3 SV Joan MIR 2018(Fullface 2 kính)").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant73 = new Variant()
                {
                    Id = 73,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 2300000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Mũ bảo hiểm cào cào Offroad LS2 FAST MX437").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant72 = new Variant()
                {
                    Id = 72,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 2700000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Mũ bảo hiểm Fullface Dual Sport LS2 PIONEER MX436").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };



                var variant71 = new Variant()
                {
                    Id = 71,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 10290000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "AGV AX-8 EVO NAKED Đen nhám - Matte Black").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant70 = new Variant()
                {
                    Id=70,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 9000000,
                    OldPrice = 0,
                    ProductId  = products.Find(ent=>ent.Name== "Áo giáp Dainese Hyper Flux D-Dry Jacket").Id,
                    SizeId = sizes.Find(ent=>ent.Name=="Free Size").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant1 = new Variant()
                {
                    Id = 1,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 250000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Kính Andes LOP-DB cho nón bảo hiểm").Id,
                    ColorId = colors.Find(ent => ent.Name == "Trà").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    VariantStatus = VariantStatus.Instock,
                    
                };
                var variant2 = new Variant()
                {
                    Id =2,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 250000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Kính Andes LOP-DB cho nón bảo hiểm").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    VariantStatus = VariantStatus.Instock,

                };
                var variant3 = new Variant()
                {
                    Id= 3,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 250000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Kính Andes LOP-DB cho nón bảo hiểm").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đa sắc").Id,
                    SizeId = sizes.Find(ent => ent.Name == "Free Size").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant4 = new Variant()
                {
                    Id = 4,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1350000,
                    OldPrice = 1500000,
                    ProductId = products.Find(ent => ent.Name == "Áo khoác giáp bảo hộ Suzuki").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "48").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant5 = new Variant()
                {
                    Id=5,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1350000,
                    OldPrice = 1500000,
                    ProductId = products.Find(ent => ent.Name == "Áo khoác giáp bảo hộ Suzuki").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "49").Id,
                    VariantStatus = VariantStatus.OutOfStock,

                };

                var variant6 = new Variant()
                {
                    Id = 6,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1350000,
                    OldPrice = 1500000,
                    ProductId = products.Find(ent => ent.Name == "Áo khoác giáp bảo hộ Suzuki").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "50").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant7 = new Variant()
                {
                    Id = 7,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1800000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Quần Giáp Komine PK 717").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "45").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant8 = new Variant()
                {
                    Id = 8,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1800000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Quần Giáp Komine PK 717").Id,
                    ColorId = colors.Find(ent => ent.Name == "Ngà").Id,
                    SizeId = sizes.Find(ent => ent.Name == "45").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant9 = new Variant()
                {
                    Id = 9,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1800000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Quần Giáp Komine PK 717").Id,
                    ColorId = colors.Find(ent => ent.Name == "Ngà").Id,
                    SizeId = sizes.Find(ent => ent.Name == "46").Id,
                    VariantStatus = VariantStatus.OutOfStock,

                };

                var variant10 = new Variant()
                {
                    Id = 10,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 1800000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "Quần Giáp Komine PK 717").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "46").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant11 = new Variant()
                {
                    Id =11,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,
                };


                var variant12 = new Variant()
                {
                    Id = 12,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant13 = new Variant()
                {
                    Id = 13,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,
                };


                var variant14 = new Variant()
                {
                    Id = 14,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant15 = new Variant()
                {
                    Id = 15,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant16 = new Variant()
                {
                    Id = 16,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant17 = new Variant()
                {
                    Id = 17,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant18 = new Variant()
                {
                    Id = 18,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant19 = new Variant()
                {
                    Id = 19,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant20 = new Variant()
                {
                    Id = 20,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 520000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,
                    
                };

                var variant21 = new Variant()
                {
                    Id = 21,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,
                    
                };

                var variant22 = new Variant()
                {
                    Id = 22,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant23 = new Variant()
                {
                    Id = 23,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant24 = new Variant()
                {
                    Id = 24,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant25 = new Variant()
                {
                    Id = 25,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant26 = new Variant()
                {
                    Id = 26,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant27 = new Variant()
                {
                    Id = 27,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };


                var variant28 = new Variant()
                {
                    Id = 28,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant29 = new Variant()
                {
                    Id = 29,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant30 = new Variant()
                {
                    Id = 30,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant31 = new Variant()
                {
                    Id = 31,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant32 = new Variant()
                {
                    Id = 32,
                    Name = "",
                    Weight = 1300,
                    OfficialPrice = 1190000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant33 = new Variant()
                {
                    Id = 33,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh lá cây").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant34 = new Variant()
                {
                    Id = 34,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh lá cây").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant35 = new Variant()
                {
                    Id = 35,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh lá cây").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant36 = new Variant()
                {
                    Id = 36,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh lá cây").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant37 = new Variant()
                {
                    Id = 37,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh lá cây").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant38 = new Variant()
                {
                    Id = 38,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Hồng").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant39 = new Variant()
                {
                    Id = 39,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Hồng").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant40 = new Variant()
                {
                    Id = 40,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Hồng").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant41 = new Variant()
                {
                    Id = 41,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Hồng").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant42 = new Variant()
                {
                    Id = 42,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Hồng").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant43 = new Variant()
                {
                    Id = 43,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant44 = new Variant()
                {
                    Id = 44,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant45 = new Variant()
                {
                    Id = 45,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };


                var variant46 = new Variant()
                {
                    Id = 46,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant47 = new Variant()
                {
                    Id = 47,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh da trời").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant48 = new Variant()
                {
                    Id = 48,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh neon").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XS").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant49 = new Variant()
                {
                    Id = 49,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh neon").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant50 = new Variant()
                {
                    Id = 50,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh neon").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant51 = new Variant()
                {
                    Id = 51,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh neon").Id,
                    SizeId = sizes.Find(ent => ent.Name == "L").Id,
                    VariantStatus = VariantStatus.Instock,

                };

                var variant52 = new Variant()
                {
                    Id = 52,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 490000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh neon").Id,
                    SizeId = sizes.Find(ent => ent.Name == "XL").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant53 = new Variant()
                {
                    Id = 53,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 390000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO KHOÁC NỮ CHỐNG THẤM NƯỚC CHUMS").Id,
                    ColorId = colors.Find(ent => ent.Name == "Nâu phối đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant54 = new Variant()
                {
                    Id = 54,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 390000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO KHOÁC NỮ CHỐNG THẤM NƯỚC CHUMS").Id,
                    ColorId = colors.Find(ent => ent.Name == "Nâu phối đỏ").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant55 = new Variant()
                {
                    Id = 55,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 390000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO KHOÁC NỮ CHỐNG THẤM NƯỚC CHUMS").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đa sắc").Id,
                    SizeId = sizes.Find(ent => ent.Name == "S").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant56 = new Variant()
                {
                    Id = 56,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 390000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "ÁO KHOÁC NỮ CHỐNG THẤM NƯỚC CHUMS").Id,
                    ColorId = colors.Find(ent => ent.Name == "Đa sắc").Id,
                    SizeId = sizes.Find(ent => ent.Name == "M").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant57 = new Variant()
                {
                    Id = 57,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "30").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant58 = new Variant()
                {
                    Id = 58,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "32").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant59 = new Variant()
                {
                    Id = 59,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "34").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant60 = new Variant()
                {
                    Id = 60,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xanh rêu").Id,
                    SizeId = sizes.Find(ent => ent.Name == "36").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant61 = new Variant()
                {
                    Id = 61,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "30").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant62 = new Variant()
                {
                    Id = 62,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "32").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant63 = new Variant()
                {
                    Id = 63,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "34").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant64 = new Variant()
                {
                    Id = 64,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám đen").Id,
                    SizeId = sizes.Find(ent => ent.Name == "36").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant65 = new Variant()
                {
                    Id = 65,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám ghi").Id,
                    SizeId = sizes.Find(ent => ent.Name == "30").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant66 = new Variant()
                {
                    Id = 66,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám ghi").Id,
                    SizeId = sizes.Find(ent => ent.Name == "32").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant67 = new Variant()
                {
                    Id = 67,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám ghi").Id,
                    SizeId = sizes.Find(ent => ent.Name == "34").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant68 = new Variant()
                {
                    Id = 68,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám ghi").Id,
                    SizeId = sizes.Find(ent => ent.Name == "36").Id,
                    VariantStatus = VariantStatus.Instock,
                };

                var variant69 = new Variant()
                {
                    Id = 69,
                    Name = "",
                    Weight = 0,
                    OfficialPrice = 280000,
                    OldPrice = 0,
                    ProductId = products.Find(ent => ent.Name == "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA").Id,
                    ColorId = colors.Find(ent => ent.Name == "Xám ghi").Id,
                    SizeId = null,
                    VariantStatus = VariantStatus.Instock,
                };



                #endregion

                var variants = new List<Variant>()
                {
                    variant1,
                    variant2,
                    variant3,
                    variant4,
                    variant5,
                    variant6,
                    variant7,
                    variant8,
                    variant9,
                    variant10,
                    variant11,
                    variant12,
                    variant13,
                    variant14,
                    variant15,
                    variant16,
                    variant17,
                    variant18,
                    variant19,
                    variant20,
                    variant21,
                    variant22,
                    variant23,
                    variant24,
                    variant25,
                    variant26,
                    variant27,
                    variant28,
                    variant29,
                    variant30,
                    variant31,
                    variant32,
                    variant33,
                    variant34,
                    variant35,
                    variant36,
                    variant37,
                    variant38,
                    variant39,
                    variant40,
                    variant41,
                    variant42,
                    variant43,
                    variant44,
                    variant45,
                    variant46,
                    variant47,
                    variant48,
                    variant49,
                    variant50,
                    variant52,
                    variant53,
                    variant54,
                    variant55,
                    variant56,
                    variant51,
                    variant57,
                    variant58,
                    variant59,
                    variant60,
                    variant61,
                    variant62,
                    variant63,
                    variant64,
                    variant65,
                    variant66,
                    variant67,
                    variant68,
                    variant69,
                    variant70,
                    variant71,
                    variant72,
                    variant73,
                    variant74,
                    variant75,
                    variant76,
                    variant77,
                };

                mDataContext.Set<Variant>().AddRange(variants);
                return variants;
            }
            else
            {
                var variants = mDataContext.Set<Variant>().ToList();
                return variants;
            }
        }
        #endregion

        #region Seed Product
        public List<Product> SeedProduct(List<Supplier> suppliers, List<SubCategory> subCategories)
        {
            if (!mDataContext.Set<Product>().Any())
            {
                #region Products
                var product1 = new Product()
                {
                    Name = "AGV Fluid Garda White Italy",
                    ImageUrl = "/StaticFiles/MyImages/agv-fluid-garda-white-italia-helmet-2-800x800.jpg",
                    ShortDescription = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở.\n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng.\n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao.\n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ ",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 5610000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm 3/4 đầu").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product2 = new Product()
                {
                    Name = "AGV Fluid IBISCUS",
                    ImageUrl = "/StaticFiles/MyImages/agv-fluid-ibiscus-1-800x800.jpg",
                    ShortDescription = "AGV Fluid IBISCUS là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 6510000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm 3/4 đầu").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product3 = new Product()
                {
                    Name = "AGV K-3 SV Balloon 2018 (Fullface 2 kính)",
                    ImageUrl = "/StaticFiles/MyImages/agv-k3-sv-balloon-2018-asian-fit-helmet-5-800x800.jpg",
                    ShortDescription = "AGV K-3 là dòng nón Fullface được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 7050000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm Fullface").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product4 = new Product()
                {
                    Name = "AGV K - 3 SV Joan MIR 2018(Fullface 2 kính)",
                    ImageUrl = "/StaticFiles/MyImages/agv-k3-sv-joan-mir-2018-fullface-helmet-0-800x800.jpg",
                    ShortDescription = "AGV K-3 là dòng nón Fullface được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "AGV Fluid là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 7250000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm Fullface").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product5 = new Product()
                {
                    Name = "Mũ bảo hiểm cào cào Offroad LS2 FAST MX437",
                    ImageUrl = "/StaticFiles/MyImages/mu-fullface-cao-cao-offroad-ls2-mx437-1-800x800.jpg",
                    ShortDescription = "Offroad LS2 là dòng nón cào cào được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "Mũ bảo hiểm cào cào Offroad LS2 FAST MX437 là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 2300000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm cào cào").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "OffRoad").Id
                };

                var product6 = new Product()
                {
                    Name = "Mũ bảo hiểm Fullface Dual Sport LS2 PIONEER MX436",
                    ImageUrl = "/StaticFiles/MyImages/mu-fullface-dual-sport-ls2-mx436-1-800x800.jpg",
                    ShortDescription = "Dual Sport LS2 là dòng nón cào cào được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "Mũ bảo hiểm Fullface Dual Sport LS2 PIONEER MX436 là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 2700000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm cào cào").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "OffRoad").Id
                };

                var product7 = new Product()
                {
                    Name = "AGV AX-8 EVO NAKED Đen nhám - Matte Black",
                    ImageUrl = "/StaticFiles/MyImages/agv-ax-8-evo-naked-matte-black-1-800x800.jpg",
                    ShortDescription = "AGV AX-8 EVO NAKED là dòng nón cào cào được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "AGV AX-8 EVO NAKED Đen nhám - Matte Black là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. \nĐẶC TÍNH KỸ THUẬT \n1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. \n2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. \n3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. \n4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. \n5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. \nModel Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát.",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 10290000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Mũ bảo hiểm cào cào").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "AGV").Id
                };

                var product8 = new Product()
                {
                    Name = "Áo giáp Dainese Hyper Flux D-Dry Jacket",
                    ImageUrl = "/StaticFiles/MyImages/dainese-hyper-flux-d-dry-jacket-3-800x800.jpg",
                    ShortDescription = "Áo giáp Dainese Hyper Flux là dòng nón cào cào được thiết kế đi trong đô thị, thành phố. Là dòng nón Fullface của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ",
                    Description = "The Dainese Hyper Flux D-Dry Jacket is the ultimate summer sport riding jacket. With removable D-Dry liner for when things get wet, the Dainese Hyper Flux Jacket is fully ventilated with mesh fabric inserts yet packed with safety features like composite elbow and shoulder armor with rigid polyurethane shoulder inserts.\n Protection: Removable composite protectors certified to EN 1621.1 standard, Polyurethane rigid shoulder covered with Duratex fabric. ",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 9000000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Áo bảo hộ").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Dainese").Id
                };

                var product9 = new Product()
                {
                    Name = "Kính Andes LOP-DB cho nón bảo hiểm",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0kinh-andes-lop-db-cho-non-bao-hiem-visor-for-3-4-helmet-1_0061160.jpg",
                    ShortDescription = "",
                    Description = "Chính hãng Andes<br>- Bằng chất liệu PC (polycarbonate), không dễ mờ đục theo thời gian, khó bể, chịu lực tốt, uốn được.<br>- Đi kèm ngàm lật có thể lắp vừa mọi loại nón bảo hiểm 3/4 có mái sử dụng 3 nút bấm như Dammtrax, Andes 111, các loại nón classic, cafe racer,...<br>Màu: Trà/Đen sang trong/Đa sắc",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 250000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Kính gắn mũ bảo hiểm").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Andes").Id
                };

                var product10 = new Product()
                {
                    Name = "Áo khoác giáp bảo hộ Suzuki",
                    ImageUrl = "/StaticFiles/MyImages/ao-khoac-giap-bao-ho-Suzuki-1.jpg",
                    ShortDescription = "",
                    Description = "<p>Áo khoác giáp bảo hộ Suzuki&nbsp; thiết kế thể thao, thoáng khí thích hợp với khí hậu nóng ẩm, tạo cảm giáp thoải mái khi đi xe đường dài, bảo vệ an toàn</p><p> Áo được may với 100 % vải sợi & nbsp; Polyester kết hợp với các tấm lưới đặt ở phần phía trước, lưng và hai bên cánh tay giúp tăng độ thông thoáng lưu thông của gió tạo cảm giác mát thoáng khi đi đường dài </ p >< p > Áo gồm 5 & nbsp; giáp: 2 cùi chỏ tay, 2 vai, lưng </ p >< p > Phần miếng đệm bảo vệ&nbsp; Bio Armor cao cấp có thể tháo rời vệ sinh, giảm tối thiểu lực tác động khi va chạm</ p >< p > Trên áo có các đường&nbsp; phản quang giúp an toàn khi đi đêm.</ p >",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 1350000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Áo bảo hộ").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Suzuki").Id
                };

                var product11 = new Product()
                {
                    Name = "Quần Giáp Komine PK 717",
                    ImageUrl = "/StaticFiles/MyImages/komine-pk-717-den.jpg",
                    ShortDescription = "",
                    Description = "<p>* QUẦN GIÁP KOMINE PK 717 CÓ THIẾT KẾ THỜI TRANG</p>< p > *CHẤT LIỆU QUẦN BẢO HỘ KOMINE PK 717 DA PHỐI LƯỚI THOÁNG MÁT </ p >< p > *QUẦN THIẾT KẾ CÓ DÂY KÉO ĐỂ KẾT HỢP VỚI ÁO KOMINE THÀNH SUIT </ p >< p > *QUẦN CÓ GIÁP ĐẦY ĐỦ NHƯNG PHẦN DỄ TỔN THƯƠNG ĐƯỢC THIẾT KẾ BẰNG DA CHỊU ĐƯỢC MÀI MÒN CAO </ p >< p > *LƯU Ý GIÁ TRÊN CHƯA BAO HỒM CỤC CHÀ GỐI 2 BÊN</ p > ",
                    WarrantyInfomation = "12 Tháng",
                    ReturnInformation = "7 Ngày",
                    BasePrice = 1350000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần bảo hộ").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Komine").Id
                };

                var product12 = new Product()
                {
                    Name = "ÁO GIÓ NAM GORE-TEX CHỐNG THẤM NƯỚC MADFOX",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-nam-gore-tex-madfox-den_0059216.jpg",
                    ShortDescription = "",
                    Description = "- Chất liệu vải Gore-Tex chống thấm nước, bên trong lót lưới giúp giữ độ thông thoáng, không gây bí, dính.<br>- 2 túi 2 bên có khóa kéo an toàn.<br>- 1 túi giấu bên trong áo để đựng những vật dụng, giấy tờ quan trọng.<br>- Các đường may được ép Seam bên trong giúp chống thấm hoàn toàn.<br>- Áo rất nhẹ, xếp gọn.<br>- Thích hợp cho mọi loại hình du lịch, Trekking, Hiking, leo núi, cắm trại,...<br>- Nón áo có thể tháo rời tuỳ ý",
                    WarrantyInfomation = "1 Tháng",
                    ReturnInformation = "3 Ngày",
                    BasePrice = 520000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần áo chống thấm nước").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Madfox").Id
                };

                var product13 = new Product()
                {
                    Name = "ÁO GIÓ NAM 5-TRONG-1 GORE-TEX CHỐNG THẤM NƯỚC MADFOX 2016",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-gore-tex-5-trong-1-chong-tham-nuoc-madfox-do_0059940.jpg",
                    ShortDescription = "",
                    Description = "MADE IN VIETNAM<br>- Áo gió chống thấm, giữ ấm với thiết kế độc đáo 5 trong 1.<br>- Lớp áo gió ngoài vải Gore-Tex chống thấm nước.<br>- Lớp áo giữ ấm bên trong sử dụng chất liệu lót bằng lông vũ cao cấp, có thể mặc riêng như một áo ấm bình thường mà không cần lớp áo gió bên ngoài. Đặc biệt áo này có thể mặc được cả 2 mặt, cộng với tay áo có thể tháo rời để thành áo Ghi-lê, tổng cộng có 4 cách khác nhau để mặc lớp áo ấm bên trong.<br>- Thích hợp cho leo núi, dã ngoại, các hoạt động ngoài trời vào mùa đông. Đi những nơi khí hậu rét, lạnh như Sapa, Tây-Đông Bắc Việt Nam, những vùng núi cao hoặc miền ôn đới.",
                    WarrantyInfomation = "1 Tháng",
                    ReturnInformation = "3 Ngày",
                    BasePrice = 1190000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần áo chống thấm nước").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Madfox").Id
                };

                var product14 = new Product()
                {
                    Name = "ÁO GIÓ NỮ GORE-TEX CHỐNG THẤM NƯỚC MADFOX",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-goretex-chong-tham-nuoc-nu-madfox_0059208.jpg",
                    ShortDescription = "",
                    Description = "- Chất liệu vải Gore-Tex chống thấm nước, bên trong lót lưới giúp giữ độ thông thoáng, không gây bí, dính.<br>- 2 túi 2 bên có khóa kéo an toàn.<br>- Các đường may được ép Seam bên trong giúp chống thấm hoàn toàn.<br>- Áo rất nhẹ, xếp gọn.<br>- Thích hợp cho mọi loại hình du lịch, Trekking, Hiking, leo núi, cắm trại,...<br>- Nón áo có thể tháo rời tuỳ ý.",
                    WarrantyInfomation = "1 Tháng",
                    ReturnInformation = "3 Ngày",
                    BasePrice = 490000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần áo chống thấm nước").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Madfox").Id
                };

                var product15 = new Product()
                {
                    Name = "ÁO KHOÁC NỮ CHỐNG THẤM NƯỚC CHUMS",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0ao-gio-chong-nuoc-chums_0057906.jpg",
                    ShortDescription = "",
                    Description = "- Áo gió chống thấm nước Chums là giải pháp tuyệt vời cho những chuyến leo núi, dã ngoại, hay đi rừng, thậm chí đi xe cũng rất hiêụ quả nhờ tính năng chống gió và chống thấm nước<br>- Có khả năng chống thấm nước đối với những cơn mưa lớn, bạn hoàn toàn có thể sử dụng thay áo mưa<br>- Chất liệu vải chống thấm 2.5 layer chuyên dụng của các loại áo gió dành cho dân leo núi với công nghệ chống thấm một chiều, bạn sẽ không bị hầm như mặc áo mưa, nó đơn giản chỉ như một chiếc áo gió rất thời trang nhưng chống nước rất tốt<br>- Áo có 3 túi bên ngoài, vạt áo và viền nón có chun rút, tay áo có băng xé dán để khoá cổ tay<br>- Mặt trong được in trang trí bản đồ Utah (một tiểu bang của Mỹ)",
                    WarrantyInfomation = "1 Tháng",
                    ReturnInformation = "3 Ngày",
                    BasePrice = 390000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần áo chống thấm nước").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Chums").Id
                };
                var product16 = new Product()
                {
                    Name = "QUẦN SHORTS NAM NHANH KHÔ COLUMBIA",
                    ImageUrl = "/StaticFiles/MyImages/fanfan0quan-lung-nhanh-kho-du-lich-columbia_0060061.jpg",
                    ShortDescription = "",
                    Description = "- Chất liệu vải nhanh khô, thoáng mát và thoải mái.<br>- Thích hợp cho du lịch, đi biển,....<br>- 5 túi, 2 túi trước, 2 túi sau và 1 túi bên hông.",
                    WarrantyInfomation = "1 Tháng",
                    ReturnInformation = "3 Ngày",
                    BasePrice = 280000,
                    SubCategoryId = subCategories.Find(ent => ent.Name == "Quần áo nhanh khô").Id,
                    SupplierId = suppliers.Find(ent => ent.Name == "Columbia").Id
                };




                #endregion

                var products = new List<Product>()
                {
                    product1,
                    product2,
                    product3,
                    product4,
                    product5,
                    product6, 
                    product7,
                    product8,
                    product9,
                    product10,
                    product11,
                    product12,
                    product13,
                    product14,
                    product15,
                    product16,

                };

                mDataContext.Set<Product>().AddRange(products);
                return products;
            } else
            {
                var products = mDataContext.Set<Product>().ToList();
                return products;
            }
        }
        #endregion


        #region Seed City
        public List<City> SeedCity()
        {
            if (!mDataContext.Set<City>().Any())
            {
                var cities = new List<City>()
                {
                    new City() { Name = "Hồ Chí Minh" },
                    new City() { Name = "Hà Nội" },
                };

                mDataContext.Set<City>().AddRange(cities);
                return cities;

            }
            else
            {
                var cities = mDataContext.Cities.ToList();
                return cities;
            }
        }
        #endregion

        #region Seed District
        public List<District> SeedDistrict(List<City> cities)
        {
            if (!mDataContext.Set<District>().Any())
            {
                var districts = new List<District>()
                {
                    new District() { Name = "Quận 1", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 2", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 3", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 4", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 5", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 6", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 7", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 8", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 9", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 10", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 11", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận 12", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Phú Nhuận", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Gò Vấp", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Bình Thạnh", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Tân Bình", ShippingFee = 25000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Tân Phú", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Thủ Đức", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Quận Bình Tân", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Huyện Củ Chi", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Huyện Hóc Môn", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Huyện Bình Chánh", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Huyện Nhà Bè", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                    new District() { Name = "Huyện Cần Giờ", ShippingFee = 30000, CityId = cities.Single(ent => ent.Name == "Hồ Chí Minh").Id },
                };

                mDataContext.Set<District>().AddRange(districts);
                return districts;

            }
            else
            {
                var districts = mDataContext.Districts.ToList();
                return districts;
            }
        }
        #endregion

        #region Seed Voucher
        public List<Voucher> SeedVoucher()
        {
            if (!mDataContext.Set<Voucher>().Any())
            {
                var vouchers = new List<Voucher>()
                {
                    new Voucher() { Method = "Giảm giá", Code = "GIAMGIA10", Quantity = 1000, RemainQuantity = 1000, Value = 10},
                    new Voucher() { Method = "Giảm giá", Code = "MAGIAMGIA5", Quantity = 1000, RemainQuantity = 1000, Value = 5}
                };

                mDataContext.Set<Voucher>().AddRange(vouchers);
                return vouchers;
            }
            else
            {
                var vouchers = mDataContext.Vouchers.ToList();
                return vouchers;
            }
        }
        #endregion

    }
}
