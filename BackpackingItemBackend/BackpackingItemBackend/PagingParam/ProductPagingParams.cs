using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Web.Models;
using BackpackingItemBackend.Constants;

namespace BackpackingItemBackend.PagingParam
{
    public enum OrderChoices
    {
        NoOrder = 0,
        IsNew = 1,
        PriceOrder = 2,
        PriceDescOrder = 3,
        NameOrder = 4,
        NameDescOrder = 5,
    }

    public class ProductPagingParams : PagingParams
    {
        public OrderChoices OrderChoice { get; set; } = 0;

        public decimal PriceMin { get; set; } = Constant.PriceMin;

        public decimal PriceMax { get; set; } = Constant.PriceMax;
    }
}
