using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoomServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Application.ReservationServices.InvoiceServices;
using MovieTheater.Application.RoomServices.Auditoriums;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Invoice;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService, IUserService userService) : base(userService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost(ApiConstant.AuditoriumCreate)]
        public async Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest model)
        {
            var result = await _invoiceService.CreateAsync(model);
            return result;
        }

    }
}