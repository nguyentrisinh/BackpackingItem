using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class VariantReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region Weight 
        public long Weight { get; set; }
        #endregion

        #region OldPrice 
        public decimal OldPrice { get; set; }
        #endregion

        #region OfficialPrice 
        public decimal OfficialPrice { get; set; }
        #endregion

        #region VariantStatus
        public VariantStatus VariantStatus { get; set; }
        #endregion

        #region Product
        public long ProductId { get; set; }
        #endregion

        #region Size 
        public SizeReturnModel Size { get; set; }
        public long ? SizeId { get; set; }
        #endregion

        #region Color
        public ColorReturnModel Color { get; set; }
        public long ColorId { get; set; }
        #endregion

        #region Images
        public List<ImageReturnModel> Images { get; set; }
        #endregion

        #region OrderDetails
        //public List<OrderDetail> OrderDetails { get; set; }
        #endregion

        public static VariantReturnModel Create(Variant variant)
        {
            #region ImageReturnModel object
            List<ImageReturnModel> images = new List<ImageReturnModel>();

            if (variant.Images != null)
            {
                foreach (var image in variant.Images)
                {
                    ImageReturnModel imageReturnModel = ImageReturnModel.Create(image);
                    images.Add(imageReturnModel);
                }
            }
            #endregion

            return new VariantReturnModel()
            {
                Id = variant.Id,
                Name = variant.Name,
                Weight = variant.Weight,
                OldPrice = variant.OldPrice,
                OfficialPrice = variant.OfficialPrice,
                VariantStatus = variant.VariantStatus,

                ProductId = variant.ProductId,

                SizeId = variant.SizeId,
                Size = variant.Size == null ? null : SizeReturnModel.Create(variant.Size),

                ColorId = variant.ColorId,
                Color = variant.Color == null ? null : ColorReturnModel.Create(variant.Color),

                Images = variant.Images == null ? null : images,
            };
        }
    }
}
