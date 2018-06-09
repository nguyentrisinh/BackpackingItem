using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public enum VariantReturnModelType
    {
        WithProduct = 1,
        WithoutProduct = 2
    }

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

        public ProductReturnModel Product { get; set; }
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

        #region Create without Product
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
                Product = null,

                SizeId = variant.SizeId,
                Size = variant.Size == null ? null : SizeReturnModel.Create(variant.Size),

                ColorId = variant.ColorId,
                Color = variant.Color == null ? null : ColorReturnModel.Create(variant.Color),

                Images = variant.Images == null ? null : images,
            };
        }
        #endregion

        #region Create with variantReturnModelType
        public static VariantReturnModel Create(Variant variant, VariantReturnModelType variantReturnModelType)
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

            if (variantReturnModelType == VariantReturnModelType.WithProduct)
            {
                return new VariantReturnModel()
                {
                    Id = variant.Id,
                    Name = variant.Name,
                    Weight = variant.Weight,
                    OldPrice = variant.OldPrice,
                    OfficialPrice = variant.OfficialPrice,
                    VariantStatus = variant.VariantStatus,

                    ProductId = variant.ProductId,
                    Product = variant.Product == null ? null : ProductReturnModel.CreateWithoutVariants(variant.Product),

                    SizeId = variant.SizeId,
                    Size = variant.Size == null ? null : SizeReturnModel.Create(variant.Size),

                    ColorId = variant.ColorId,
                    Color = variant.Color == null ? null : ColorReturnModel.Create(variant.Color),

                    Images = variant.Images == null ? null : images,
                };

            }

            return new VariantReturnModel()
            {
                Id = variant.Id,
                Name = variant.Name,
                Weight = variant.Weight,
                OldPrice = variant.OldPrice,
                OfficialPrice = variant.OfficialPrice,
                VariantStatus = variant.VariantStatus,

                ProductId = variant.ProductId,
                Product = null,

                SizeId = variant.SizeId,
                Size = variant.Size == null ? null : SizeReturnModel.Create(variant.Size),

                ColorId = variant.ColorId,
                Color = variant.Color == null ? null : ColorReturnModel.Create(variant.Color),

                Images = variant.Images == null ? null : images,
            };
        }
        #endregion

    }
}
