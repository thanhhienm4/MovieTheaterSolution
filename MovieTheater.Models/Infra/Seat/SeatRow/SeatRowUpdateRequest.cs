using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat.SeatRow
{
    public class SeatRowUpdateRequest
    {
        public int Id { get; set; }

        [Display(Name = "Hàng ghế")]
        public string Name { get; set; }
    }
    public class SeatRowUpdateValidator : AbstractValidator<SeatRowUpdateRequest>
    {
        public SeatRowUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Hàng ghế không được để trống");
        }
    }
}
