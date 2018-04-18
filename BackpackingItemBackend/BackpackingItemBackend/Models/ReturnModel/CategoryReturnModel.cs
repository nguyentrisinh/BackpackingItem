using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class CategoryReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region SubCategories
        public List<SubCategoryReturnModel> SubCategories { get; set; }
        #endregion

        public static CategoryReturnModel Create (Category category)
        {
            List<SubCategoryReturnModel> subCategories = new List<SubCategoryReturnModel>();

            if (category.SubCategories != null)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    SubCategoryReturnModel subCategoryReturnItem = SubCategoryReturnModel.Create(subCategory);
                    subCategories.Add(subCategoryReturnItem);
                }
            }

            return new CategoryReturnModel
            {
                Id = category.Id,
                Name = category.Name,
                SubCategories = category.SubCategories == null ? null : subCategories
            };
        }
    }
}
