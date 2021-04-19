using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningMD
    {
        [Display(Name = "Mã xuất chiếu")]
        public int Id { get; set; }
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime TimeStart { get; set; }
        [Display(Name = "Tên phim")]
        public int FilmId { get; set; }
        [Display(Name = "Tên phòng")]
        public int RoomId { get; set; }
        [Display(Name = "Loại xuất chiếu")]

        public int KindOfScreeningId { get; set; }
    }
}
