using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Web.Infrastructures
{
    public class ApiErrorMessage
    {
        public const int GENERIC_ERROR_CODE = 9999;

        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public static IList<ApiErrorMessage> FromIdentityResult(IdentityResult result)
        {
            var errors = new List<ApiErrorMessage>();

            foreach (var errorResult in result.Errors)
            {
                errors.Add(new ApiErrorMessage() { ErrorMessage = errorResult.Description, ErrorCode = 9998 });
            }

            return errors;
        }
        public static ApiErrorMessage FromException(Exception exception)
        {
            return new ApiErrorMessage() { ErrorMessage = exception.Message, ErrorCode = GENERIC_ERROR_CODE };
        }

    }
}
