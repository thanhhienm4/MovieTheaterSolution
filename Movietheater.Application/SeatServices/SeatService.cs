using MovieTheater.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public class SeatService
    {
        private readonly MovieTheaterDBContext _context;
        public SeatService(MovieTheaterDBContext context)
        {
            _context = context;
        }


    }
}
