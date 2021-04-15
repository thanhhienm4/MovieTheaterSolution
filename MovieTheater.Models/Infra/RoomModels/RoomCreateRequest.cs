using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomCreateRequest
    {
        [Display(Name = "Tên phòng")]
        public string Name { get; set; }
        [Display(Name = "Mã loại phòng")]
        public int FormatId { get; set; }
    }
    public class RoomCreateValidator : AbstractValidator<RoomCreateRequest>
    {
        public RoomCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phòng không được bỏ trống");

        }
    }
}
