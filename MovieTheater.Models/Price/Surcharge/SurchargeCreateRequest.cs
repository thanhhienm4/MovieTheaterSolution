using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MovieTheater.Models.Price.Surcharge
{
    public class SurchargeCreateRequest
    {
        [Display(Name = "Loại ghế")]
        public string SeatType { get; set; }
        [Display(Name = "Định dạng phòng")]
        public string AuditoriumFormatId { get; set; }
        [Display(Name = "Phụ phí")]
        public decimal Price { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
    }

    public class SurChargeCreateValidator : AbstractValidator<SurchargeCreateRequest>
    {
        public SurChargeCreateValidator()
        {
            RuleFor(x => x.SeatType).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.AuditoriumFormatId).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
        }
    }
}