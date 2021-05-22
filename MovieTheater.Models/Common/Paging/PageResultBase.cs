using System;

namespace MovieTheater.Models.Common.Paging
{
    public class PageResultBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }

        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecord / PageSize);
            }
        }
    }
}