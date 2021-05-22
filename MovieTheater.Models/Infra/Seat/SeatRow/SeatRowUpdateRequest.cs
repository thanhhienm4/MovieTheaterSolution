﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

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