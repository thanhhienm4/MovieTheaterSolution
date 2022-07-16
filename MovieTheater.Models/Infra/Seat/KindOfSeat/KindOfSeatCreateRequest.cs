using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.Seat.KindOfSeat
{
    public class SeatTypeCreateRequest
    {
        [Display(Name = "Tên loại ghế")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public string Id { get; set; }
    }
}