using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BackpackingItemBackend.Infrastructures;
using Microsoft.AspNetCore.Identity;

namespace BackpackingItemBackend.BaseServices
{
    public class ThrowService : IThrowService
    {
        #region ThrowApiException

        public void ThrowApiException(ApiErrorMessage errorMessage, HttpStatusCode statusCode)
        {
            throw new ApiResponseException() { ErrorMessages = new List<ApiErrorMessage>() { errorMessage }, StatusCode = statusCode };
        }

        public void ThrowApiException(IList<ApiErrorMessage> errorMessage, HttpStatusCode statusCode)
        {
            throw new ApiResponseException() { ErrorMessages = errorMessage, StatusCode = statusCode };
        }


        public void ThrowApiException(IdentityResult result)
        {
            var errors = ApiErrorMessage.FromIdentityResult(result);

            ThrowApiException(errors, HttpStatusCode.BadRequest);
        }

        public void ThrowApiException(int errorCode, string erroMessage, HttpStatusCode statusCode)
        {
            IList<ApiErrorMessage> errorMessages = new List<ApiErrorMessage>();
            errorMessages.Add(new ApiErrorMessage() { ErrorCode = errorCode, ErrorMessage = erroMessage });
            ThrowApiException(errorMessages, statusCode);
        }


        public void ThrowApiException(string erroMessage, HttpStatusCode statusCode)
        {
            IList<ApiErrorMessage> errorMessages = new List<ApiErrorMessage>();
            errorMessages.Add(new ApiErrorMessage() { ErrorCode = ApiErrorMessage.GENERIC_ERROR_CODE, ErrorMessage = erroMessage });
            ThrowApiException(errorMessages, statusCode);
        }

        #endregion
    }
}
