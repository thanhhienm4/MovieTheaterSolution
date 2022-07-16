using MovieTheater.Models.Common;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class RoomPagingRequest : PagingRequest
    {
        public string? FormatId { get; set; }
    }
}