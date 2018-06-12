using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Web.Controllers;
using Lib.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Lib.Web.Models;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.PagingParam;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
using BackpackingItemBackend.Constants;
using BackpackingItemBackend.Services;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IProductService productService;

        #region Contructor

        public ProductsController(ApplicationDbContext context, IProductService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            productService = services;
        }

        #endregion

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetLatestProducts(int numberOfProduct)
        {

            List<ProductReturnModel> products = productService.GetLatestProducts(numberOfProduct);

            return await this.AsSuccessResponse(products, HttpStatusCode.OK);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSpecialProducts(int numberOfProduct)
        {
            var productList = _context.Products
                .OrderBy(ent => ent.BasePrice)
                .Take(numberOfProduct)
                .ToList();

            List<ProductReturnModel> products = new List<ProductReturnModel>();

            foreach (var productObject in productList)
            {
                ProductReturnModel productReturnItem = ProductReturnModel.Create(productObject);
                products.Add(productReturnItem);
            }

            return await this.AsSuccessResponse(products, HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{productId:long}")]
        public async Task<IActionResult> GetById(long productId)
        {
            //try
            //{
            //    var product = _context.Products
            //        .First(ent => ent.Id == productId);

            //    return await this.AsSuccessResponse(product, HttpStatusCode.OK);
            //}
            //catch (InvalidOperationException)
            //{
            //    ThrowService.ThrowApiException(ErrorsDefine.Find(2100), HttpStatusCode.BadRequest);
            //    return await this.AsSuccessResponse(null, HttpStatusCode.OK);
            //}
            Product product = productService.GetById(productId);
            ProductReturnModel productReturnModel = ProductReturnModel.Create(product);
            return await this.AsSuccessResponse(productReturnModel, HttpStatusCode.OK);
        }

        #region GET List
        [HttpGet]
        public async Task<IActionResult> GetProducts(ProductPagingParams pagingParams)
        {
            var model = productService.getProducts(pagingParams);

            return await this.AsSuccessResponse(model, HttpStatusCode.OK);

        }

        [HttpGet]
        [Route("[action]/{categoryId:long}")]
        public async Task<IActionResult> GetProductsByCategory(ProductPagingParams pagingParams, long categoryId)
        {
            var model = productService.getProductsByCategory(pagingParams, categoryId);

            return await this.AsSuccessResponse(model, HttpStatusCode.OK);

        }

        [HttpGet]
        [Route("[action]/{subCategoryId:long}")]
        public async Task<IActionResult> getProductsBySubCategory(ProductPagingParams pagingParams, long subCategoryId)
        {
            var model = productService.getProductsBySubCategory(pagingParams, subCategoryId);

            return await this.AsSuccessResponse(model, HttpStatusCode.OK);

        }

        #endregion
    }
}