using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class AuditoriumFormatCreateRequest
    {
        [Display(Name = "Mã")]
        public string Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }

        public class AuditoriumFormatCreateValidator : AbstractValidator<AuditoriumFormatCreateRequest>
        {
            public AuditoriumFormatCreateValidator()
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Giá của không được âm ");
            }
        }
    }
}