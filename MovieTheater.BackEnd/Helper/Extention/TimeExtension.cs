using Microsoft.Extensions.Logging;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Price.Time;

namespace MovieTheater.BackEnd.Helper.Extention
{
    public static class TimeExtension
    {
        public static TimeVMD ToVMD(this Time time)
        {
            if(time == null)
                return null;
            else
            {
                return new TimeVMD()
                {
                    Name = time.Name,
                    DateEnd = time.DateEnd,
                    DateStart = time.DateStart,
                    HourEnd = time.HourStart,
                    HourStart = time.HourStart,
                    TimeId = time.TimeId
                };
            }
        }
    }
}
