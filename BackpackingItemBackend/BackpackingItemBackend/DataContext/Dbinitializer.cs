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
            #region categories
            var categories = SeedCategory();
            #endregion


        }

        #region Seed Category

        public Category[] SeedCategory ()
        {
            var categories = new Category[]
{
                new Category() { Name = "Đồ phượt" },
                new Category() { Name = "Mũ bảo hiểm" },
                new Category() { Name = "Áo khoác" },
                new Category() { Name = "Găng tay" }
};

            foreach (Category category in categories)
            {
                mDataContext.Categories.Add(category);
            }

            mDataContext.SaveChanges();

            return categories;
        }
        #endregion

    }
}
