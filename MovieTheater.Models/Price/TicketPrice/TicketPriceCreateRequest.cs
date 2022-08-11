using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Price.TicketPrice
{
    public class TicketPriceCreateRequest
    {
        [Display(Name = "Loại khách hàng")]
        public string CustomerType { get; set; }

        [Display(Name = "Định dạng")]
        public string AuditoriumFormat { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime? FromTime { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Thời gian")]
        public string TimeId { get; set; }

        [Display(Name = "Mã Giá")]
        public int Id { get; set; }
    }

    public class TicketPriceCreateValidator : AbstractValidator<TicketPriceCreateRequest>
    {
        public TicketPriceCreateValidator()
        {
            RuleFor(x => x.CustomerType).NotEmpty().WithMessage("Loại khách hàng được bỏ trống");
            RuleFor(x => x.AuditoriumFormat).NotEmpty().WithMessage("Tên định dạng phòng không được bỏ trống");
            RuleFor(x => x.FromTime).NotEmpty().WithMessage("Ngày bắt đầu không được bỏ trống");
            RuleFor(x => x.ToTime).NotEmpty().WithMessage("Ngày kết thúc không được bỏ trống");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Giá không được bỏ trống");
            RuleFor(x => x.TimeId).NotEmpty().WithMessage("Mã giá không được bỏ trống");
        }
    }
}