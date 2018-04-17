using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Category
    {
        #region ID
        public long Id { get; set; }

        #endregion

        #region Name
        public string Name { get; set; }

        #endregion

        #region SubCategories
        public List<SubCategory> SubCategories { get; set; }
        #endregion
    }
}
