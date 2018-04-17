using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Image
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region ImageUrl
        public string ImageUrl { get; set; }
        #endregion

        #region Variant
        public Variant Variant { get; set; }
        public long VariantId { get; set; }
        #endregion
    }
}
