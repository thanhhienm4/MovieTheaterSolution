using MovieTheater.Models.Common;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class AuditoriumPagingRequest : PagingRequest
    {
        public string? FormatId { get; set; }
    }
}