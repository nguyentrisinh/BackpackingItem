using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Web.Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackpackingItemBackend.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _serializerSettings;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (ApiResponseException apiResponseException)
            {

                ApiResponse response = ApiResponse.Create(apiResponseException.ErrorMessages);

                context.Response.StatusCode = (int)apiResponseException.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
            }
            catch (Exception ex)
            {
                ApiResponse response = ApiResponse.Create(ex);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
