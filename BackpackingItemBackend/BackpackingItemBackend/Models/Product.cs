using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Product
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

        #region ShortDescription
        public string ShortDescription { get; set; }
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

        #region SubCategory
        public virtual SubCategory SubCategory { get; set; }
        public long SubCategoryId { get; set; }
        #endregion

        #region Supplier
        public virtual Supplier Supplier { get; set; }
        public long SupplierId { get; set; }
        #endregion

        #region Variants
        public List<Variant> Variants { get; set; }
        #endregion
    }
}
