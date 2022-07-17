using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class AuditoriumCreateRequest
    {
        [Display(Name = "Tên phòng")] public string Name { get; set; }

        [Display(Name = "Mã loại phòng")] public string FormatId { get; set; }
    }

    public class AuditoriumCreateValidator : AbstractValidator<AuditoriumCreateRequest>
    {
        public AuditoriumCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phòng không được bỏ trống");
        }
    }
}