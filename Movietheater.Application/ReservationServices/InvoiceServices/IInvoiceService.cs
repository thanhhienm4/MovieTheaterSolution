using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices.InvoiceServices
{
    public interface IInvoiceService
    {
        Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest request);
    }
}