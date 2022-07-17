using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomMD
    {
        [Display(Name = "Mã phòng")] public string Id { get; set; }

        [Display(Name = "Tên phòng")] public string Name { get; set; }

        [Display(Name = "Mã loại phòng")] public string FormatId { get; set; }
    }
}