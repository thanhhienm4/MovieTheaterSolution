namespace MovieTheater.Models.Infra.Seat
{
    public class SeatVMD
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int Number { get; set; }
        public string RowName { get; set; }
        public string TypeId { get; set; }
        public string AuditoriumId { get; set; }
    }
}