using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;

namespace MovieTheater.Application.ReservationServices.InvoiceServices
{
    public interface IInvoiceService
    {
        Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest request);
    }
}