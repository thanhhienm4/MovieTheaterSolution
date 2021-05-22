using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.Seat.SeatRow
{
    public class SeatRowCreateRequest
    {
        [Display(Name = "Hàng ghế")]
        public string Name { get; set; }
    }

    public class SeatRowCreateValidator : AbstractValidator<SeatRowCreateRequest>
    {
        public SeatRowCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Hàng ghế không được để trống");
        }
    }
}