using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningCreateRequest
    {
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Tên phim")]
        public int FilmId { get; set; }

        [Display(Name = "Tên phòng")]
        public int RoomId { get; set; }

        [Display(Name = "Loại xuất chiếu")]
        public int KindOfScreeningId { get; set; }
        public class ScreeningCreateValidator : AbstractValidator<ScreeningCreateRequest>
        {
            public ScreeningCreateValidator()
            {
                RuleFor(x => x.StartTime).GreaterThan(DateTime.Now).WithMessage("Thời gian bắt đầu luôn lớn hơn hiện tại");
            }
        }
    }
}