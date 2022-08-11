#nullable disable

namespace MovieTheater.Data.Results
{
    public partial class SeatModel
    {
        public SeatModel()
        {
        }

        public int RowId { get; set; }
        public int Number { get; set; }
        public string AuditoriumId { get; set; }
        public string TypeId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public int Status { get; set; }
        public string RowName { get; set; }
    }
}