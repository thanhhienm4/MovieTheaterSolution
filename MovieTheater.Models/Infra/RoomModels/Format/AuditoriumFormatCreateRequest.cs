using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class AuditoriumFormatCreateRequest
    {
        [Display(Name = "Mã")] public string Id { get; set; }
        [Display(Name = "Tên")] public string Name { get; set; }

        public class AuditoriumFormatCreateValidator : AbstractValidator<AuditoriumFormatCreateRequest>
        {
            public AuditoriumFormatCreateValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Mã định dạng không được để trống");
                RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Tên định dạng dạng không được để trống");
            }
        }
    }
}