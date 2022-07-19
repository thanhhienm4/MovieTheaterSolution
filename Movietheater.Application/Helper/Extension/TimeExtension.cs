using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Price.Time;

namespace MovieTheater.Application.Helper.Extension
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
                    HourEnd = time.HourEnd,
                    HourStart = time.HourStart,
                    TimeId = time.TimeId
                };
            }
        }
    }
}
