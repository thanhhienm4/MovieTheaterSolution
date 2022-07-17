using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Catalog.Film
{
    public class MovieCreateRequest
    {
        [Display(Name = "Mã phim")]
        public string Id { get; set; }

        [Display(Name = "Tên phim")] 
        public string Name { get; set; }

        [Display(Name = "Mô tả ")]
        public string Description { get; set; }

        [Display(Name = "Ngày ra rạp")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Đường dẫn của Trailer")]
        public string TrailerURL { get; set; }

        [Display(Name = "Thời lượng phim (phút)")]
        public int Length { get; set; }

        [Display(Name = "Giới hạn độ tuổi")] public string CensorshipId { get; set; }

        [Display(Name = "Poster")] public IFormFile Poster { get; set; }
    }

    public class MovieCreateValidator : AbstractValidator<MovieCreateRequest>
    {
        public MovieCreateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã phim không được bỏ trống");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phim không được bỏ trống");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả phim không được bỏ trống");
            RuleFor(x => x.PublishDate).NotEmpty().WithMessage("Ngày công chiếu không được bỏ trống");
            RuleFor(x => x.TrailerURL).NotEmpty().WithMessage("Trailer phim không được bỏ trống");
            RuleFor(x => x.Length).NotEmpty().WithMessage("Thời lượng phim không được bỏ trống").GreaterThan(0)
                .WithMessage("Thời lượng phim phải lớn hơn 0");
            RuleFor(x => x.Poster).NotEmpty().WithMessage("Poster phim không được bỏ trống");
        }
    }
}