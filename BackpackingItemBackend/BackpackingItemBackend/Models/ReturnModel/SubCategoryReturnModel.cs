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

        #region CategoryId
        public long CategoryId { get; set; }
        #endregion

        #region Products
        public List<ProductReturnModel> Products { get; set; }
        #endregion

        public static SubCategoryReturnModel Create(SubCategory subCategory)
        {
            #region ProductReturnModel Products
            List<ProductReturnModel> products = new List<ProductReturnModel>();

            if (subCategory.Products != null)
            {
                foreach (var product in subCategory.Products)
                {
                    ProductReturnModel productReturnItem = ProductReturnModel.Create(product);
                    products.Add(productReturnItem);
                }
            }
            #endregion

            return new SubCategoryReturnModel
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                Products = subCategory.Products == null ? null : products
            };
        }
    }
}
