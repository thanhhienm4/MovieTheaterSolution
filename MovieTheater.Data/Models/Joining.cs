#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Joining
    {
        public int ActorId { get; set; }
        public string MovieId { get; set; }
        public string PositionId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Position Position { get; set; }
    }
}