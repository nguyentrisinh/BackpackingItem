using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BackpackingItemBackend.Infrastructures;
using Microsoft.AspNetCore.Identity;

namespace BackpackingItemBackend.BaseServices
{
    public interface IThrowService
    {
        void ThrowApiException(ApiErrorMessage errorMessage, HttpStatusCode statusCode);

        void ThrowApiException(IList<ApiErrorMessage> errorMessage, HttpStatusCode statusCode);

        void ThrowApiException(IdentityResult result);

        void ThrowApiException(int errorCode, string erroMessage, HttpStatusCode statusCode);

        void ThrowApiException(string erroMessage, HttpStatusCode statusCode);
    }
}
