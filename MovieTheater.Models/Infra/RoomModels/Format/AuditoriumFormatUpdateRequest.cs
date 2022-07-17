using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class AuditoriumFormatUpdateRequest
    {
        public string Id { get; set; }
        [Display(Name = "Tên")] public string Name { get; set; }
        [Display(Name = "Giá")] public int Price { get; set; }

        public class AuditoriumFormatUpdateValidator : AbstractValidator<AuditoriumFormatUpdateRequest>
        {
            public AuditoriumFormatUpdateValidator()
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Giá của không được âm ");
            }
        }
    }
}