using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Web.Services;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
using Lib.Web.Models;
using BackpackingItemBackend.PagingParam;

namespace BackpackingItemBackend.Services
{
    public interface IProductService
    {
        List<ProductReturnModel> GetLatestProducts(int numberOfProduct);

        // Get Products Paging List
        PagedList<Product> getProducts(ProductPagingParams pagingParams);

    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public ProductService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        public List<ProductReturnModel> GetLatestProducts(int numberOfProduct)
        {
            var productList = _context.Products
                .OrderByDescending(ent => ent.Id)
                .Take(numberOfProduct)
                .ToList();

            List<ProductReturnModel> products = new List<ProductReturnModel>();

            foreach (var productObject in productList)
            {
                ProductReturnModel productReturnItem = ProductReturnModel.Create(productObject);
                products.Add(productReturnItem);
            }
            return products;
        }


        // Test paging
        public PagedList<Product> getProducts(ProductPagingParams pagingParams)
        {
            var query = _context.Products.AsQueryable();

            #region Order
            // Order 
            switch (pagingParams.OrderChoice)
            {
                case OrderChoices.IsNew:
                    query = query.OrderBy(ent => ent.Id);
                    break;
                case OrderChoices.NameOrder:
                    query = query.OrderBy(ent => ent.Name);
                    break;
                case OrderChoices.NameDescOrder:
                    query = query.OrderByDescending(ent => ent.Name);
                    break;
                case OrderChoices.PriceOrder:
                    query = query.OrderBy(ent => ent.BasePrice);
                    break;
                case OrderChoices.PriceDescOrder:
                    query = query.OrderByDescending(ent => ent.BasePrice);
                    break;
                default:
                    break;
            }
            #endregion

            #region PriceFilter
            query = query.Where(ent => ent.BasePrice >= pagingParams.PriceMin && ent.BasePrice <= pagingParams.PriceMax);
            #endregion

            return new PagedList<Product>(query, pagingParams.PageNumber, pagingParams.PageSize);
        }


    }
}
