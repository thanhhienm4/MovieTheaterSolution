using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningVMD
    {
        [Display(Name = "Mã xuất chiếu")]
        public int Id { get; set; }
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime TimeStart { get; set; }
        [Display(Name = "Tên phim")]
        public string Film { get; set; }
        [Display(Name = "Tên phòng")]
        public string Room { get; set; }
        [Display(Name = "Loại xuấtt chiếu")]
        public string KindOfScreening { get; set; }
    }
}
