using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningVMD
    {
        [Display(Name = "Mã xuất chiếu")]
        public int Id { get; set; }

        [Display(Name = "Thời gian bắt đầu")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Thời gian kết thúc")]
        public DateTime FinishTime { get; set; }

        [Display(Name = "Tên phim")]
        public string Movie { get; set; }

        [Display(Name = "Tên phòng")]
        public string Auditorium { get; set; }

    }
}