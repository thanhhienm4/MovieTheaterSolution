using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat.SeatRow
{
    public class SeatRowUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SeatRowUpdateValidator : AbstractValidator<SeatRowUpdateRequest>
    {
        public SeatRowUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
}
