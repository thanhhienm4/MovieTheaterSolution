using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class RoomFormatCreateRequest
    {
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }

        public class RoomFormatCreateValidator : AbstractValidator<RoomFormatCreateRequest>
        {
            public RoomFormatCreateValidator()
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Giá của không được âm ");
            }
        }
    }
}