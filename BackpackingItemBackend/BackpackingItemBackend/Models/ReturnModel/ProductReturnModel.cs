using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class ProductReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region ImageUrl
        public string ImageUrl { get; set; }
        #endregion

        #region Description
        public string Description { get; set; }
        #endregion

        #region WarrantyInfomation 
        public string WarrantyInfomation { get; set; }
        #endregion

        #region ReturnInfomation
        public string ReturnInformation { get; set; }
        #endregion

        #region BasePrice
        public decimal BasePrice { get; set; }
        #endregion

        #region SubCategoryId
        public long SubCategoryId { get; set; }
        #endregion

        #region SupplierId
        public long SupplierId { get; set; }
        #endregion

        #region Variants
        public List<VariantReturnModel> Variants { get; set; }
        #endregion

        public static ProductReturnModel Create(Product product)
        {
            #region VariantReturnModel variants
            List<VariantReturnModel> variants = new List<VariantReturnModel>();

            if (product.Variants != null)
            {
                foreach (var variant in product.Variants)
                {
                    VariantReturnModel variantReturnItem = VariantReturnModel.Create(variant);
                    variants.Add(variantReturnItem);
                }
            }
            #endregion


            return new ProductReturnModel()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                WarrantyInfomation = product.WarrantyInfomation,
                ReturnInformation = product.ReturnInformation,
                BasePrice = product.BasePrice,

                SubCategoryId = product.SubCategoryId,

                SupplierId = product.SupplierId,

                Variants = product.Variants == null ? null : variants,
            };
        }

        public static List<ProductReturnModel> Create(List<Product> products)
        {
            List<ProductReturnModel> productsReturnModel = new List<ProductReturnModel>();

            foreach (var product in products)
            {
                ProductReturnModel productReturnModel = ProductReturnModel.Create(product);
                productsReturnModel.Add(productReturnModel);
            }

            return productsReturnModel;
        }
    }
}
