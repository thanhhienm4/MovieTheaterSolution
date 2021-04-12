namespace MovieTheater.Models.Infra.Seat.SeatRow
{
    public class SeatRowUpdateValidator : AbstractValidator<SeatRowUpdateRequest>
    {
        public SeatRowUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
}
