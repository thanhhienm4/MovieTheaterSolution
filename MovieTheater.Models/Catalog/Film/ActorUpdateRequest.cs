using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Film
{
    public class ActorUpdateRequest
    {
        public int Id { get; set; }

        [Display(Name = "Ngày sinh")] public DateTime DOB { get; set; }

        [Display(Name = "Mô tả")] public string Description { get; set; }

        [Display(Name = "Tên")] public string Name { get; set; }

        public class ActorUpdateRequestValidator : AbstractValidator<ActorUpdateRequest>
        {
            public ActorUpdateRequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được bỏ trống");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được bỏ trống");
                RuleFor(x => x.DOB).NotEmpty().WithMessage("Ngày sinh không được bỏ trống");
            }
        }
    }
}