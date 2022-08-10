using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MovieTheater.Models.Price.Surcharge
{
    public class SurchargeUpdateRequest
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
        [Display(Name = "Mã phụ phí")]
        public DateTime Id { get; set; }
    }

    public class SurChargeUpdateValidator : AbstractValidator<SurchargeUpdateRequest>
    {
        public SurChargeUpdateValidator()
        {
            RuleFor(x => x.SeatType).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.AuditoriumFormatId).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Loại ghế không được bỏ trống");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã ghế không được bỏ trống");
            
        }
    }
}