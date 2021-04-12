﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class PeopleCreateRequest
    {
        public DateTime DOB { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    public class PeopleCreateValidator : AbstractValidator<PeopleCreateRequest>
    {
        public PeopleCreateValidator()
        {
            RuleFor(x => x.DOB).NotEmpty().WithMessage("Ngày sinh không được bỏ trống");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được bỏ trống");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được bỏ trống");

        }
    }
}
