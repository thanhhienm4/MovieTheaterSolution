using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Screening
{
    public class KindOfScreeningCreateRequest
    {
        [Display(Name = "Tên loại suất chiếu")]
        public string Name { get; set; }

        [Display(Name = "Giá")] public int Surcharge { get; set; }
    }
}