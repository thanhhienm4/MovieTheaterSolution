namespace MovieTheater.Models.Common.ApiResult
{
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