using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat.SeatRow
{
    public class SeatRowCreateRequest
    {
        public string Name { get; set; }
    }
    public class SeatRowCreateValidator : AbstractValidator<SeatRowCreateRequest>
    {
        public SeatRowCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
}
