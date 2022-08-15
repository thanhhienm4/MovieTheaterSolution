using System;
using System.Security.Cryptography;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

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
                ReservationId = request.ReservationId,
                TransactionId = request.TransactionId
            };

            await _context.Invoices.AddAsync(invoice);
            var rv = _context.Reservations.Find(request.ReservationId);
            rv.PaymentStatus = PaymentStatusType.Done;
            rv.Time = DateTime.Now;
            _context.SaveChanges();
            if (invoice.Id > 0)
                return new ApiSuccessResult<bool>(true, "Thêm thành công");
            else
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }
    }
}