using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Web.Infrastructures
{
    public class ApiResponse
    {
        #region Data
        public dynamic Data { get; set; }
        #endregion

        #region Errors
        public IList<ApiErrorMessage> Errors { get; set; }
        #endregion

        #region Sql
        public string Sql { get; set; }
        #endregion

        #region Create
        public static ApiResponse Create(dynamic data, IList<ApiErrorMessage> errors)
        {
            var response = new ApiResponse();
            response.Data = data;
            response.Errors = errors;

            return response;
        }

        public static ApiResponse Create(dynamic data)
        {
            return Create(data, null);
        }

        public static ApiResponse Create(IList<ApiErrorMessage> errors)
        {
            return Create(null, errors);
        }

        public static ApiResponse Create(ApiErrorMessage error)
        {
            return Create(null, new List<ApiErrorMessage>() { error });
        }

        public static ApiResponse Create(Exception exception)
        {
            return Create(ApiErrorMessage.FromException(exception));
        }
        #endregion
    }
}
