using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class ImageReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region ImageUrl
        public string ImageUrl { get; set; }
        #endregion

        #region Variant
        //public Variant Variant { get; set; }
        public long VariantId { get; set; }
        #endregion

        public static ImageReturnModel Create(Image image)
        {
            return new ImageReturnModel()
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                VariantId = image.VariantId
            };
        }
    }
}
