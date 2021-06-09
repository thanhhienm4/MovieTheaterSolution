using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.Seat.KindOfSeat
{
    public class KindOfSeatCreateRequest
    {
        [Display(Name = "Tên loại ghế")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int SurCharge { get; set; }
    }
}