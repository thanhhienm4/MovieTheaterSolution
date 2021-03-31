using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Common.ApiResult
{
    public class ApiResultLite
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
    }

    public class ApiSuccessResultLite : ApiResultLite
    {
        public ApiSuccessResultLite()
        {
            IsSuccessed = true;
        }
        public ApiSuccessResultLite(string message)
        {
            IsSuccessed = true;
            Message = message;
        }
    }

    public class ApiErrorResultLite : ApiResultLite
    {
        public ApiErrorResultLite(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResultLite()
        {
            IsSuccessed = false;
        }
    }
}
