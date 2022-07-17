using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class AuditoriumVMD
    {
        [Display(Name = "Mã phòng")] public string Id { get; set; }

        [Display(Name = "Tên phòng")] public string Name { get; set; }

        [Display(Name = "Loại  phòng")] public string Format { get; set; }
    }
}