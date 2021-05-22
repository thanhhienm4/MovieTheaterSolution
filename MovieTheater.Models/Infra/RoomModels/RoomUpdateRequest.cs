using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomUpdateRequest
    {
        public int Id { get; set; }

        [Display(Name = "Tên phòng")]
        public string Name { get; set; }

        [Display(Name = "Mã loại phòng")]
        public int FormatId { get; set; }
    }

    public class RoomUpdateValidator : AbstractValidator<RoomUpdateRequest>
    {
        public RoomUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phòng không được bỏ trống");
        }
    }
}