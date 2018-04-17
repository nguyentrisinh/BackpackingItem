using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class SubCategory
    {
        #region Id
        public long Id { get; set; }

        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region Category
        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }

        #endregion

        #region Products
        public List<Product> Products { get; set; }
        #endregion
    }
}
