namespace MovieTheater.Models.Common.ApiResult
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            ResultObj = resultObj;
            IsSuccessed = true;
        }
    }
}