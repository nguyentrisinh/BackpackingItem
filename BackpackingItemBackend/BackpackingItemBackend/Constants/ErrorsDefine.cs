using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Web.Infrastructures;


namespace BackpackingItemBackend.Constants
{
    public class ErrorsDefine
    {
        private IDictionary<int, string> errorCodeTable = new Dictionary<int, string>()
        {
            //9999: Server can not handle this error
            //9998: Error Identity

            //19xx: Cagtegory Error
            {1900, "Category not found" },

            //20xx: SubCategory Error
            {2000, "SubCategory not found"},

            //21xx: Product Error
            {2100, "Product not found" },

            //22xx: User Error
            {2200, "User not found" },
            {2201, "Username or password is not correct" },

            //23xx: Variant Error
            {2300, "Variant not found" },

            //24xx: Order Error
            {2400, "Order not found" },

            //25xx: City Error
            {2500, "City not found" },

            //26xx: District Error
            {2600, "District not found" },

        };

        #region Methods 
        public string this[int errorCode]
        {
            get { return errorCodeTable[errorCode]; }
        }

        private static ErrorsDefine instance;
        public static ErrorsDefine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ErrorsDefine();
                }
                return instance;
            }
        }

        public static ApiErrorMessage Find(int errorCode)
        {
            var apiErrorMessage = new ApiErrorMessage();

            try
            {
                var errorDefine = Instance[errorCode];
                apiErrorMessage.ErrorCode = errorCode;
                apiErrorMessage.ErrorMessage = errorDefine;
            }
            catch (Exception ex)
            {
                apiErrorMessage.ErrorCode = 9999;
                apiErrorMessage.ErrorMessage = ex.Message;
            }

            return apiErrorMessage;
        }

        #endregion
    }
}
