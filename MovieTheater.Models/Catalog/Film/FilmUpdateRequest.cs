using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class FilmUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Length { get; set; }
        public string TrailerURL { get; set; }
        public int BanId { get; set; }
        public IFormFile Poster { get; set; }
    }

    public class FilmUpdateValidator : AbstractValidator<FilmUpdateRequest>
    {
        public FilmUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phim không được bỏ trống");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Miêu tả phim không được bỏ trống");
            RuleFor(x => x.PublishDate).LessThan(DateTime.Now).WithMessage("Phim phải có ngày chiếu ít nhất trước một ngày");
            RuleFor(x => x.TrailerURL).NotEmpty().WithMessage("Trailer phim không được bỏ trống");
            RuleFor(x => x.Length).NotEmpty().WithMessage("Thời lượng phim không được bỏ trống");
            RuleFor(x => x.Poster).NotEmpty().WithMessage("Poster phim không được bỏ trống");
        }
    }
}
