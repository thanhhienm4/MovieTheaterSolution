using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class AuditoriumUpdateRequest
    {
        public string Id { get; set; }

        [Display(Name = "Tên phòng")] public string Name { get; set; }

        [Display(Name = "Mã loại phòng")] public string FormatId { get; set; }
    }

    public class AuditoriumUpdateValidator : AbstractValidator<AuditoriumUpdateRequest>
    {
        public AuditoriumUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phòng không được bỏ trống");
        }
    }
}