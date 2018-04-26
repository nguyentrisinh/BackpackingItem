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
using BackpackingItemBackend.Constants;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BackpackingItemBackend.Services
{
    public interface IProductService
    {
        List<ProductReturnModel> GetLatestProducts(int numberOfProduct);

        #region GET List
        // Get Products Paging List
        PagedList<Product> getProducts(ProductPagingParams pagingParams);

        // Get Products Paging List for SubCategoryId
        //PagedList<Product> getProductsBySubCategory(ProductPagingParams pagingParams, long subCategoryId);
        PagedList<ProductReturnModel> getProductsBySubCategory(ProductPagingParams pagingParams, long subCategoryId);

        // Get Products Paging List for CategoryId
        PagedList<Product> getProductsByCategory(ProductPagingParams pagingParams, long categoryId);
        #endregion

        #region GET by Id
        Product GetById(long productId);
        #endregion
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

        #region GET List Products
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

        //public PagedList<Product> getProductsBySubCategory(ProductPagingParams pagingParams, long subCategoryId)
        public PagedList<ProductReturnModel> getProductsBySubCategory(ProductPagingParams pagingParams, long subCategoryId)
        {
            try
            {
                // Check SubcategoryId is exists
                var subCategory = _context.SubCategories.First(ent => ent.Id == subCategoryId);
                                    

                var query = _context.Products.Where(ent => ent.SubCategoryId == subCategoryId).AsQueryable();

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

                #region GET paging for ProductReturnModel
                List<Product> products = query.Skip(pagingParams.PageSize * (pagingParams.PageNumber - 1))
                    .Take(pagingParams.PageSize)
                    .ToList();
                #endregion

                //return new PagedList<Product>(query, pagingParams.PageNumber, pagingParams.PageSize);
                return new PagedList<ProductReturnModel>(ProductReturnModel.Create(products), pagingParams.PageNumber, pagingParams.PageSize, query.Count());
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2000), HttpStatusCode.BadRequest);
                return new PagedList<ProductReturnModel>();
            }
        }

        public PagedList<Product> getProductsByCategory(ProductPagingParams pagingParams, long categoryId)
        {
            try
            {
                // Check SubcategoryId is exists
                var category = _context.Categories.First(ent => ent.Id == categoryId);


                // Get Products through Category
                //var query = _context.SubCategories.Where(ent => ent.CategoryId == categoryId)
                //    .SelectMany(ent => ent.Products)
                //    .AsQueryable();
                var query = _context.Categories.Where(ent => ent.Id == categoryId)
                    .SelectMany(ent => ent.SubCategories)
                    .SelectMany(ent => ent.Products)
                    .AsQueryable();


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
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(1900), HttpStatusCode.BadRequest);
                return new PagedList<Product>();
            }
        }
        #endregion

        #region Get Product By Id
        public Product GetById(long productId)
        {
            try
            {
                var product = _context.Products
                    .Include(ent => ent.Variants).ThenInclude(ent => ent.Size)
                    .Include(ent => ent.Variants).ThenInclude(ent => ent.Color)
                    .Include(ent => ent.Variants).ThenInclude(ent => ent.Images)
                    .First(ent => ent.Id == productId);

                return product;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2100), HttpStatusCode.BadRequest);
                return new Product();
            }
        }
        #endregion
    }
}
