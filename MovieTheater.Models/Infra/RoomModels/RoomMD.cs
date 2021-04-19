using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomMD
    {
        [Display(Name = "Mã phòng")]
        public int Id { get; set; }
        [Display(Name = "Tên phòng")]
        public string  Name { get; set; }
        [Display(Name = "Mã loại phòng")]
        public int FormatId { get; set; }
    }
}
