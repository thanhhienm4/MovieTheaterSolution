using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Price.TicketPrice;

namespace MovieTheater.Application.PriceServices
{
    public interface ITicketPriceService
    {
        Task<ApiResult<bool>> CreateAsync(TicketPriceCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(TicketPriceUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);


        Task<ApiResult<PageResult<TicketPriceVMD>>> GetTicketPricePagingAsync(TicketPricePagingRequest request);

        Task<ApiResult<TicketPriceVMD>> GetTicketPriceById(int id);
    }
}