using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningMD
    {
        [Display(Name = "Mã xuất chiếu")] public int Id { get; set; }

        [Display(Name = "Thời gian bắt đầu")] public DateTime StartTime { get; set; }

        [Display(Name = "Tên phim")] public string MovieId { get; set; }

        [Display(Name = "Tên phòng")] public string AuditoriumId { get; set; }

        [Display(Name = "Loại xuất chiếu")] public int KindOfScreeningId { get; set; }
    }
}