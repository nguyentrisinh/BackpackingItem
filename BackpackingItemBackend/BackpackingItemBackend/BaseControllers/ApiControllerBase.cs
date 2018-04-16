using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BackpackingItemBackend.Infrastructures;
using BackpackingItemBackend.BaseServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackpackingItemBackend.BaseControllers
{
    public abstract class ApiControllerBase : Controller
    {

        protected IThrowService ThrowService;

        #region Contructor

        public ApiControllerBase(IThrowService throwService)
        {
            this.ThrowService = throwService;
        }

        #endregion

        //#region Contructor

        //public ApiControllerBase()
        //{

        //}

        //#endregion

        #region Methods

        #region AsSuccessResponse
        protected virtual Task<IActionResult> AsSuccessResponse(ApiResponse apiResponse, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return Task.FromResult<IActionResult>(new ObjectResult(apiResponse) { StatusCode = (int)statusCode });
        }

        /// <summary>
        /// Provide explicit statusCode in case data is dynamic
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> AsSuccessResponse(dynamic data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse response = ApiResponse.Create(data);
            return await this.AsSuccessResponse(response, (HttpStatusCode)statusCode);
        }

        protected virtual async Task<IActionResult> AsSuccessResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse response = ApiResponse.Create(null, null);
            return await this.AsSuccessResponse(response, statusCode);
        }

        #endregion

        #endregion

    }

}
