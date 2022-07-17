using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.Seat.KindOfSeat
{
    public class KindOfSeatUpdateRequest
    {
        public string Id { get; set; }
        [Display(Name = "Tên loại ghế")] public string Name { get; set; }
    }
}