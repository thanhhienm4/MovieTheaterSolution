using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ReservationServices.InvoiceServices;
using MovieTheater.Application.ReservationServices.Reservations;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IReservationService _reservationService;

        public InvoiceController(IInvoiceService invoiceService, IUserService userService,
            IReservationService reservationService) : base(userService)
        {
            _invoiceService = invoiceService;
            _reservationService = reservationService;
        }

        [HttpPost(ApiConstant.InvoiceCreate)]
        public async Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest model)
        {
            var result = await _invoiceService.CreateAsync(model);
            if (result.IsSuccessed)
                await _reservationService.UpdatePaymentStatus(new ReservationUpdatePaymentRequest()
                {
                    Id = model.ReservationId,
                    Status = PaymentStatusType.Done
                });
            return result;
        }
    }
}