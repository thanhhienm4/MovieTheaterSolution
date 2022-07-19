using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MovieTheater.Models.Catalog.Price.Time
{
    public class TimeUpdateRequest
    {
        public string TimeId { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
    }
    public class TimeUpdateValidator : AbstractValidator<TimeUpdateRequest>
    {
        public TimeUpdateValidator()
        {
            RuleFor(x => x.TimeId).NotEmpty().WithMessage("Mã thời gian không được bỏ trống");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được bỏ trống");
            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("Ngày kết thúc không được bỏ trống");
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("Ngày bắt đầu không được bỏ trống");
            RuleFor(x => x.HourEnd).NotEmpty().WithMessage("Giờ kết thúc không được bỏ trống");
            RuleFor(x => x.HourStart).NotEmpty().WithMessage("Giờ bắt đầu không được bỏ trống");
        }
    }
}
