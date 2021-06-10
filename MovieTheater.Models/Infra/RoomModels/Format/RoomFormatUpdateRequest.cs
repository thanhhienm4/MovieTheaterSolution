using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class RoomFormatUpdateRequest
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }

        public class RoomFormatUpdateValidator : AbstractValidator<RoomFormatUpdateRequest>
        {
            public RoomFormatUpdateValidator()
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Giá của không được âm ");
            }
        }
    }
}