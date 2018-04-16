﻿using System;
using System.Collections.Generic;
using System.Net;

namespace BackpackingItemBackend.Infrastructures
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
