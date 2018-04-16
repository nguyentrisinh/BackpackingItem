using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Lib.Web.Infrastructures;
using Microsoft.AspNetCore.Identity;

namespace Lib.Web.Services
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
