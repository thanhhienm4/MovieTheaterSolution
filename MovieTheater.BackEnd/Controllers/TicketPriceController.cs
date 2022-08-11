using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Threading.Tasks;
using MovieTheater.Application.PriceServices;
using MovieTheater.Models.Price.TicketPrice;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketPriceController : BaseController
    {
        private readonly ITicketPriceService _ticketPriceService;

        public TicketPriceController(IUserService userService, ITicketPriceService ticketPriceService) : base(userService)
        {
            _ticketPriceService = ticketPriceService;
        }

        [HttpPost(ApiConstant.TicketPriceCreate)]
        public async Task<ApiResult<bool>> CreateAsync(TicketPriceCreateRequest request)
        {
            var result = await _ticketPriceService.CreateAsync(request);
            return result;
        }

        [HttpPut(ApiConstant.TicketPriceUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(TicketPriceUpdateRequest request)
        {
            var result = await _ticketPriceService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(ApiConstant.TicketPriceDelete + "/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _ticketPriceService.DeleteAsync(id);
            return result;
        }

        [HttpGet(ApiConstant.TicketPriceGetById)]
        public async Task<ApiResult<TicketPriceVmd>> GetByIdAsync(int id)
        {
            var result = await _ticketPriceService.GetTicketPriceById(id);
            return result;
        }

        [HttpPost(ApiConstant.TicketPricePaging)]
        public async Task<ApiResult<PageResult<TicketPriceVmd>>> PagingAsync(TicketPricePagingRequest request)
        {
            var result = await _ticketPriceService.GetTicketPricePagingAsync(request);
            return result;
        }
    }
}