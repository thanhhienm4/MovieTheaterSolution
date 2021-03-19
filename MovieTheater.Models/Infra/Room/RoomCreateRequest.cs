using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Room
{
    public class RoomCreateRequest
    {
        public string Name { get; set; }
        public int FormatId { get; set; }
    }
}
