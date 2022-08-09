using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;

namespace MovieTheater.Application.ReservationServices.InvoiceServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly MoviesContext _context;

        public InvoiceService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest request)
        {
            var invoice = new Invoice()
            {
                Date = request.Date,
                PaymentId = request.PaymentId,
                Price = request.Price,
                ReservationId = request.ReservationId
            };
            await _context.Invoices.AddAsync(invoice);
            if(invoice.Id > 0)
                return new ApiSuccessResult<bool>(true, "Thêm thành công");
            else
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }
    }
}