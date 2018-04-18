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


namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;


        #region Contructor

        public ProductsController(ApplicationDbContext context, IThrowService throwService) : base(throwService)
        {
            _context = context;
        }

        #endregion

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetLatestProducts(int numberOfProduct)
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
    }
}