using System;
using System.Collections.Generic;
using System.Net;

namespace Lib.Web.Infrastructures
{
    public class ApiResponseException : System.Exception
    {
        #region ErrorMessages
        public IList<ApiErrorMessage> ErrorMessages { get; set; }
        #endregion

        #region StatusCode
        public HttpStatusCode StatusCode { get; set; }
        #endregion
    }
}
