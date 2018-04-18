using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class SupplierReturnModel
    {
        #region Id
        public long Id { get; set; }

        #endregion

        #region Name
        public string Name { get; set; }

        #endregion

        #region Country
        public string Country { get; set; }
        #endregion

        #region Products
        public List<ProductReturnModel> Products { get; set; }
        #endregion

        public static SupplierReturnModel Create(Supplier supplier)
        {
            List<ProductReturnModel> products = new List<ProductReturnModel>();

            if (supplier.Products != null)
            {
                foreach (var product in supplier.Products)
                {
                    ProductReturnModel productReturnItem = ProductReturnModel.Create(product);
                    products.Add(productReturnItem);
                }
            }

            return new SupplierReturnModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Country = supplier.Country,
                Products = supplier.Products == null ? null : products
            };
        }
    }
}
