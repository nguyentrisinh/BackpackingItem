using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class SubCategoryReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        public static SubCategoryReturnModel Create(SubCategory subCategory)
        {
            return new SubCategoryReturnModel
            {
                Id = subCategory.Id,
                Name = subCategory.Name
            };
        }
    }
}
