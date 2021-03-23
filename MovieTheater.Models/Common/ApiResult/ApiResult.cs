namespace MovieTheater.Models.Common.ApiResult
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }

    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            ResultObj = resultObj;
            IsSuccessed = true;
        }
    }

    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult()
        {
            IsSuccessed = false;
        }
    }
}