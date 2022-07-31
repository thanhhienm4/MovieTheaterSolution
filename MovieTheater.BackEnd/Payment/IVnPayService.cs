using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.User;

namespace MovieTheater.BackEnd.Payment
{
    public interface IVnPayService
    {
        string CreateRequest(ReservationVMD reservation, CustomerVMD user);
    }
}