﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FormatId { get; set; }
    }
    public class RoomUpdateValidator : AbstractValidator<RoomUpdateRequest>
    {
        public RoomUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phòng không được bỏ trống");

        }
    }
}
