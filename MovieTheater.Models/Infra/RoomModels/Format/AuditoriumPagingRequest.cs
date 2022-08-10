using MovieTheater.Models.Common;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class AuditoriumPagingRequest : PagingRequest
    {
        public string? FormatId { get; set; }
    }
}