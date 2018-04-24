using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models.ReturnModel;
using Lib.Web.Controllers;
using Lib.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BackpackingItemBackend.Constants;
using BackpackingItemBackend.Services;
using Lib.Web.Models;


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
            try
            {
                var product = _context.Products
                    .First(ent => ent.Id == productId);

                return await this.AsSuccessResponse(product, HttpStatusCode.OK);
            }
            catch (InvalidOperationException)
            {
                ThrowService.ThrowApiException(ErrorsDefine.Find(2100), HttpStatusCode.BadRequest);
                return await this.AsSuccessResponse(null, HttpStatusCode.OK);
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPagingList(PagingParams pagingParams)
        {
            var model = productService.getProducts(pagingParams);

            return await this.AsSuccessResponse(model, HttpStatusCode.OK);

        }

    }
}