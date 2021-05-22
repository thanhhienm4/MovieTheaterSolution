using MovieTheater.Models.Common;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class RoomPagingRequest : PagingRequest
    {
        public int? FormatId { get; set; }
    }
}