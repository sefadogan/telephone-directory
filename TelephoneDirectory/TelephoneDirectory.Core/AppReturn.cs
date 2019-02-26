using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Core
{
    public class AppReturn
    {
        public const int ERROR_CODE_NO_ERROR = 0;
        public const int ERROR_CODE_INVALID_OPERATION = 501;
        public const int ERROR_CODE_INTERNAL_ERROR = 500;

        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public AppReturn()
        {
        }
        public AppReturn(Exception e)
        {
            Success = false;
            ErrorCode = ERROR_CODE_INTERNAL_ERROR;
            Message = e.Message;
        }
        public static AppReturn Successful(string message = null)
        {
            return new AppReturn
            {
                ErrorCode = ERROR_CODE_NO_ERROR,
                Success = true,
                Message = message ?? "Process successful.",
            };
        }
        public static AppReturn InvalidOperation(string message = null)
        {
            return new AppReturn
            {
                ErrorCode = ERROR_CODE_INVALID_OPERATION,
                Success = false,
                Message = message ?? "Error! Invalid operation.",
            };
        }
    }
}
